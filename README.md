# Prism Learning

## Modules

A modular application is an application that is divided into a set of loosely coupled functional units (named modules) that can be integrated into a larger application. The main benefit of the modular approach is that it can make your overall application architecture more flexible and maintainable because it allows you to break your application into manageable pieces.

### Description

We added a Login module to check how works Prism Modules because the main target is to encapsulate every service, view and viewmodel of a functional unit inside it.

### Steps

* Create a new portable class library (PCL) on your solution.
* Add your Prism.Forms *"flavor"* nuget package (*your favourite dependency container*). In this example, we choose Unity.
* Move all your services, views and viewmodels of a functional unit to this new assembly.
* Register all your services, views and viewmodels.

```csharp
public class LoginModule : Prism.Modularity.IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
        containerRegistry.RegisterSingleton<ILoginService, LoginService>();
    }
}
```

* Register the module on your app (App.cs).

```csharp
protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
{
    moduleCatalog.AddModule<LoginModule.LoginModule>(InitializationMode.WhenAvailable);
}
```

* We can add initialize the module in two different ways:

  * **WhenAvailable.** The module will be initialized when it is availble on application start-up.

  * **OnDemand.** The module will be initialized when requested.

```csharp
var moduleManager = Container.Resolve<IModuleManager>();
moduleManager.LoadModule(nameof(LoginModule.LoginModule));
```

## Application Lifecycle Management

Mobile applications development has to deal with the concept of application lifecycle. With this we mean that mobile apps are created to manage scenarios where batterly life, CPU and memory are limited (as opposed to the classic desktop app where all of this is unlimited).

The typical application lifecycle events are:

* Initializing. This happens the first time the app is launched.
* Resuming. This happens every time we restore the app from the background after it has been suspended.
* Sleeping. This happens when the OS decides to freeze our app after it has gone in background.

The management of these events can be tricky in an MVVM app but Prism provides the IApplicationLifecycleAware interface to make your life easier.

### How to handle ALM at Application level

You can handle ALM events at the Application level by overriding the OnSleep() and OnResume() methods in the App class. The following is an example of an App class with ALM management.

```csharp
  public partial class App : PrismApplication
    {

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
        }

        protected override void OnResume()
        {
            base.OnResume();

            // TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.

        }

        protected override void OnSleep()
        {
            base.OnSleep();

            // TODO: This is the time to save app data in case the process is terminated.
            // This is the perfect timing to release exclusive resources (camera, I/O devices, etc...)

        }
    }
```

### How to handle ALM in your ViewModels

The following is an example of a ViewModel that implements IApplicationLifecycleAware.

Is needed implement ALM events at the Application level to handle them in your ViewModels.

```csharp
 public class ViewModelExample : ViewModelBase, IApplicationLifecycleAware
    {
        protected INavigationService NavigationService { get; private set; }

        public ViewModelExample(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public void OnResume()
        {
            //Restore the state of your ViewModel.
        }

        public  void OnSleep()
        {
            //Save the state of your ViewModel.
        }
    }
```
