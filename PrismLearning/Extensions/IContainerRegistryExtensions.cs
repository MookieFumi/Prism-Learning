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
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            containerRegistry.RegisterForNavigation<PlayersView, PlayersViewModel>();
        }

        public static void AddServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IPlayersService>(new PlayersServiceDecorator(new PlayersService()));
            containerRegistry.RegisterSingleton<ITeamsService, TeamsService>();
        }
    }
}
