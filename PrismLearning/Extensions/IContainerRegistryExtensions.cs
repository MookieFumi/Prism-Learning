using MonkeyCache;
using Prism.Ioc;
using PrismLearning.DomainService;
using PrismLearning.DomainService.Abstractions;
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
            IBarrel barrel;
#if DEBUG
            barrel = BarrelModelFactory.Build(debugging: false);
#else
            barrel = BarrelModelFactory.Build(debugging: true);

#endif
            containerRegistry.RegisterInstance(barrel);
            containerRegistry.RegisterInstance<IPlayersService>(new PlayersServiceCache(barrel, new PlayersService()));

            containerRegistry.RegisterSingleton<ITeamsService, TeamsService>();
        }
    }
}
