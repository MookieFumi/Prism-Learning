using System.Collections.Generic;
using System.Threading.Tasks;
using PrismLearning.DomainService.Abstractions.DTO;
using Refit;

namespace PrismLearning.DomainService.Abstractions
{
    public interface IPlayersService
    {
        [Get("/players-stats-teams/{team}")]
        Task<IEnumerable<PlayerDTO>> GetPlayers(string team);

        [Get("/players-stats")]
        Task<IEnumerable<PlayerDTO>> GetPlayers();
    }
}

