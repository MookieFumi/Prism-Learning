using System;
using System.Collections.Generic;
using System.Linq;
using MonkeyCache;

namespace PrismLearning.Services.Cache.Base
{
    public class CacheBaseService
    {
        public TimeSpan DefaultTimeSpan => TimeSpan.FromDays(1);

        public void AddToCache<T>(IBarrel barrel, string key, IEnumerable<T> data)
        {
            if (data.Any())
            {
                barrel.Add(key: key, data: data, expireIn: DefaultTimeSpan);
            }
        }
    }
}
