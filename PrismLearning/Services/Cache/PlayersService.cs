using System.Collections.Generic;
using System.Threading.Tasks;
using MonkeyCache;
using PrismLearning.DomainService.Abstractions;
using PrismLearning.DomainService.Abstractions.DTO;
using PrismLearning.Services.Cache.Base;

namespace PrismLearning.Services.Cache
{
    public class PlayersService : CacheBaseService, IPlayersService
    {
        private readonly IBarrel _barrel;
        private readonly IPlayersService _playersService;

        public PlayersService(IBarrel barrel, IPlayersService playersService)
        {
            _barrel = barrel;
            _playersService = playersService;
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayers(string team)
        {
            var key = $"{this.ToString()}/{nameof(GetPlayers)}/{team}";

            if (!_barrel.IsExpired(key: key))
            {
                return _barrel.Get<IEnumerable<PlayerDTO>>(key: key);
            }

            var data = await _playersService.GetPlayers(team);

            AddToCache(_barrel, key, data);

            return data;
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayers()
        {
            var key = $"{this.ToString()}/{nameof(GetPlayers)}";

            if (!_barrel.IsExpired(key: key))
            {
                return _barrel.Get<IEnumerable<PlayerDTO>>(key: key);
            }

            var data = await _playersService.GetPlayers();

            AddToCache(_barrel, key, data);

            return data;
        }
    }
}
