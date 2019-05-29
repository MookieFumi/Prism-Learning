using System.Threading.Tasks;
using Xunit;

namespace PrismLearning.DomainService.Test
{
    public class TeamServiceTest
    {

        [Fact]
        public async Task GetTeams_Should_Not_Return_Null()
        {
            var sut = new TeamsService();

            var teams = await sut.GetTeams();

            Assert.NotNull(teams);
        }
    }
}
