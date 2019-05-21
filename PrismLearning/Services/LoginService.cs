using System.Threading.Tasks;
using PrismLearning.Services.DTO;

namespace PrismLearning.Services
{
    public class LoginService : ILoginService
    {
        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            await Task.Delay(1);
            return new LoginResponseDTO
            {
                User = request.User,
                Name = "Miguel Angel Martin Hrdez"
            };
        }
    }
}
