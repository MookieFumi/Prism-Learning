using Prism.Ioc;
using PrismLearning.Services;
using PrismLearning.ViewModels;
using PrismLearning.Views;

namespace PrismLearning.Extensions
{
    public static class IContainerRegistryExtensions
    {
        public static void AddViews(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
        }

        public static void AddServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoginService, LoginService>();
        }
    }
}
