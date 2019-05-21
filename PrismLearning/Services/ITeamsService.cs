using System.Collections.Generic;
using System.Threading.Tasks;
using PrismLearning.Services.DTO;

namespace PrismLearning.Services
{
    public interface ITeamsService
    {
        Task<IEnumerable<TeamDTO>> GetTeams();
    }
}
