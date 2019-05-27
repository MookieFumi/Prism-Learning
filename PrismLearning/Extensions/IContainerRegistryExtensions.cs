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
#if DEBUG
            MonkeyCache.FileStore.Barrel.ApplicationId = "my_fileStore_applicationId";
            containerRegistry.RegisterInstance(MonkeyCache.FileStore.Barrel.Current);
            containerRegistry.RegisterInstance<IPlayersService>(new PlayersServiceCache(MonkeyCache.FileStore.Barrel.Current, new PlayersService()));
#else
            MonkeyCache.LiteDB.Barrel.ApplicationId = "my_liteDb_applicationId";
            containerRegistry.RegisterInstance(MonkeyCache.LiteDB.Barrel.Current);
            containerRegistry.RegisterInstance<IPlayersService>(new PlayersServiceCache(MonkeyCache.LiteDB.Barrel.Current, new PlayersService()));
#endif
            //containerRegistry.RegisterInstance<IPlayersService>(new PlayrsServiceCache(new PlayersService()));
            containerRegistry.RegisterSingleton<ITeamsService, TeamsService>();
        }
    }
}
