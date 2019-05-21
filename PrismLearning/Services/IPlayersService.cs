using System.Collections.Generic;
using System.Threading.Tasks;
using PrismLearning.Services.DTO;
using Refit;

namespace PrismLearning.Services
{
    public interface IPlayersService
    {
        [Get("/players-stats-teams/{team}")]
        Task<IEnumerable<PlayerDTO>> GetPlayers(string team);

        [Get("/players-stats")]
        Task<IEnumerable<PlayerDTO>> GetPlayers();
    }
}
