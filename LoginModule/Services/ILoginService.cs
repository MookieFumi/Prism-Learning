using System.Threading.Tasks;
using LoginModule.Services.Model;

namespace LoginModule.Services
{
    public interface ILoginService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
    }
}
