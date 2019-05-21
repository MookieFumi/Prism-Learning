using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrismLearning.Services.DTO;

namespace PrismLearning.Services
{
    public class TeamsService : ITeamsService
    {
        public Task<IEnumerable<TeamDTO>> GetTeams()
        {
            var teams = new List<TeamDTO>
            {
                new TeamDTO{ Acronym="hou", Name="Houston Rockets", Image="https://ssl.gstatic.com/onebox/media/sports/logos/zhO6MIB1UzZmtXLHkJQBmg_96x96.png"},
                new TeamDTO{ Acronym="phi", Name="Philadelphia 76ers", Image="https://ssl.gstatic.com/onebox/media/sports/logos/US6KILZue2D5766trEf0Mg_96x96.png"},
                new TeamDTO{ Acronym="orl", Name="Orlando Magic", Image="https://ssl.gstatic.com/onebox/media/sports/logos/p69oiJ4LDsvCJUDQ3wR9PQ_96x96.png"},
                new TeamDTO{ Acronym="pho", Name="Phoenix Suns",Image="https://ssl.gstatic.com/onebox/media/sports/logos/pRr87i24KHWH0UuAc5EamQ_96x96.png"},
                new TeamDTO{ Acronym="okc", Name="Oklahoma City Thunder", Image="https://ssl.gstatic.com/onebox/media/sports/logos/b4bJ9zKFBDykdSIGUrbWdw_96x96.png"},
                new TeamDTO{ Acronym="ind", Name="Indiana Pacers",Image="https://ssl.gstatic.com/onebox/media/sports/logos/andumiE_wrpDpXvUgqCGYQ_96x96.png"},
                new TeamDTO{ Acronym="bro", Name="Brooklyn Nets",Image="https://ssl.gstatic.com/onebox/media/sports/logos/iishUmO7vbJBE7iK2CZCdw_96x96.png"},
                new TeamDTO{ Acronym="gsw", Name="Golden State Warriors",Image="https://ssl.gstatic.com/onebox/media/sports/logos/XD2v321N_-vk7paF53TkAg_96x96.png"},
                new TeamDTO{ Acronym="mem", Name="Memphis Grizzlies",Image="https://ssl.gstatic.com/onebox/media/sports/logos/3ho45P8yNw-WmQ2m4A4TIA_96x96.png"}
            };

            return Task.FromResult(teams.AsEnumerable());
        }
    }
}
