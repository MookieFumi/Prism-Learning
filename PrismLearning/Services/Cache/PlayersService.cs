using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonkeyCache;
using PrismLearning.DomainService.Abstractions;
using PrismLearning.DomainService.Abstractions.DTO;

namespace PrismLearning.Services.Cache
{
    public class PlayersService : IPlayersService
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

            var result = await _playersService.GetPlayers(team);

            if (result.Any())
            {
                _barrel.Add(key: key, data: result, expireIn: TimeSpan.FromDays(1));
            }

            return result;
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayers()
        {
            return await _playersService.GetPlayers();
        }
    }
}
