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
            containerRegistry.RegisterForNavigation<PlayerDetailView, PlayerDetailViewModel>();
        }

        public static void AddServices(this IContainerRegistry containerRegistry)
        {
            IBarrel barrel;
#if DEBUG
            barrel = BarrelFactory.Build(debugging: true);
#else
            barrel = BarrelModelFactory.Build(debugging: false);

#endif
            containerRegistry.RegisterInstance<IBarrel>(barrel);
            containerRegistry.RegisterInstance<IPlayersService>(new Services.Cache.PlayersService(barrel, new PlayersService()));

            containerRegistry.RegisterSingleton<ITeamsService, TeamsService>();
        }
    }
}
