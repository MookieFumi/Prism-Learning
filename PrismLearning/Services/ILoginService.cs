using System.Threading.Tasks;
using PrismLearning.Services.DTO;

namespace PrismLearning.Services
{
    public interface ILoginService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
    }
}
