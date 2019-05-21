using System.Threading.Tasks;

namespace PrismLearning.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }

    public class LoginService : ILoginService
    {
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            await Task.Delay(1);
            return new LoginResponse
            {
                User = request.User,
                Name = "Miguel Angel Martin Hrdez"
            };
        }
    }

    public class LoginResponse
    {
        public string User { get; set; }
        public string Name { get; set; }
    }

    public class LoginRequest
    {
        public LoginRequest(string user, string password)
        {
            User = user;
            Password = password;
        }

        public string User { get; private set; }
        public string Password { get; private set; }
    }
}
