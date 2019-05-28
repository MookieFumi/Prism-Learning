using System.Collections.Generic;
using System.Threading.Tasks;
using PrismLearning.DomainService.Abstractions;
using PrismLearning.DomainService.Abstractions.DTO;
using Refit;

namespace PrismLearning.DomainService
{
    public class PlayersService : IPlayersService
    {
        private const string baseUrl = "https://nba-players.herokuapp.com";

        private readonly IPlayersService _service;

        public PlayersService()
        {
            _service = RestService.For<IPlayersService>(baseUrl);
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayers(string team)
        {
            return await _service.GetPlayers(team);
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayers()
        {
            return await _service.GetPlayers();
        }
    }
}
