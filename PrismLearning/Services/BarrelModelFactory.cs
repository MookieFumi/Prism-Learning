using MonkeyCache;

namespace PrismLearning.Services
{
    public static class BarrelModelFactory
    {
        public static IBarrel Build(bool debugging)
        {
            if (debugging)
            {
                MonkeyCache.FileStore.Barrel.ApplicationId = "App-FileDStore";
                return MonkeyCache.FileStore.Barrel.Current;
            }

            MonkeyCache.LiteDB.Barrel.ApplicationId = "App-LiteDB";
            return MonkeyCache.LiteDB.Barrel.Current;
        }
    }
}
