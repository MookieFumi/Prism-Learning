using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonkeyCache.LiteDB;
using PrismLearning.Services.DTO;

namespace PrismLearning.Services
{
    public class PlayersServiceDecorator : IPlayersService
    {
        private readonly IPlayersService playersService;

        public PlayersServiceDecorator(IPlayersService playersService)
        {
            this.playersService = playersService;
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayers(string team)
        {
            var cache = $"{this.ToString()}/{nameof(GetPlayers)}/{team}";

            if (!Barrel.Current.IsExpired(key: cache))
            {
                return Barrel.Current.Get<IEnumerable<PlayerDTO>>(key: cache);
            }
            var result = await playersService.GetPlayers(team);

            //Saves the cache and pass it a timespan for expiration
            Barrel.Current.Add(key: cache, data: result, expireIn: TimeSpan.FromDays(1));
            return result;
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayers()
        {
            return await playersService.GetPlayers();
        }
    }
}
