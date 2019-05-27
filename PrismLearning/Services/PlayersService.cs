using System.Collections.Generic;
using System.Threading.Tasks;
using PrismLearning.Services.DTO;
using Refit;

namespace PrismLearning.Services
{
    public class PlayersService : IPlayersService
    {
        private const string baseUrl = "https://nba-players.herokuapp.com";

        public async Task<IEnumerable<PlayerDTO>> GetPlayers(string team)
        {
            var service = RestService.For<IPlayersService>(baseUrl);

            return await service.GetPlayers(team);
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayers()
        {
            ////Dev handles checking if cache is expired
            //if (!Barrel.Current.IsExpired(key: url))
            //{
            //    return Barrel.Current.Get<IEnumerable<Monkey>>(key: url);
            //}

            var service = RestService.For<IPlayersService>(baseUrl);

            return await service.GetPlayers();
        }


    }
}
