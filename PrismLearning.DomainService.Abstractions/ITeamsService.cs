using System.Collections.Generic;
using System.Threading.Tasks;
using PrismLearning.DomainService.Abstractions.DTO;

namespace PrismLearning.DomainService.Abstractions
{
    public interface ITeamsService
    {
        Task<IEnumerable<TeamDTO>> GetTeams();
    }
}
