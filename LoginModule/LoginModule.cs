using LoginModule.Services;
using LoginModule.ViewModels;
using LoginModule.Views;
using Prism.Ioc;
using Unity;

namespace LoginModule
{
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
}
