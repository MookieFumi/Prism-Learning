using System.Threading.Tasks;
using LoginModule.Services.Model;

namespace LoginModule.Services
{
    public class LoginService : ILoginService
    {
        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            await Task.Delay(1);
            return new LoginResponseDTO
            {
                User = request.User,
                Name = "Elena Nito Del Bosque"
            };
        }
    }
}
