# Prism Learning

## Modules

A modular application is an application that is divided into a set of loosely coupled functional units (named modules) that can be integrated into a larger application. The main benefit of the modular approach is that it can make your overall application architecture more flexible and maintainable because it allows you to break your application into manageable pieces.

### Description

We added a Login module to check how works Prism Modules because the main target is to encapsulate every service, view and viewmodel inside it.

### Steps

* Create a new portable class library (PCL) on your solution
* Add your Prism.Forms flavor (your favourite dependency container). We choose Unity.
* Move all your services, views and viewmodels to this new assembly.
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

  * **OnDemand.** The module will be initialized when requested.

  * **WhenAvailable.** The module will be initialized when it is availble on application start-up.

