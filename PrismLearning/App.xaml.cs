using LoginModule.Views;
using Prism;
using Prism.Ioc;
using Prism.Modularity;
using PrismLearning.Extensions;
using Xamarin.Forms;

namespace PrismLearning
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(LoginView)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.AddViews();
            containerRegistry.AddServices();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<LoginModule.LoginModule>(InitializationMode.WhenAvailable);

            //moduleCatalog.AddModule<LoginModule.LoginModule>(InitializationMode.OnDemand);
            //var moduleManager = Container.Resolve<IModuleManager>();
            //moduleManager.LoadModule(nameof(LoginModule.LoginModule));
        }

        #region App Events
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
