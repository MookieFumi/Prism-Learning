namespace PrismLearning.Services.DTO
{
    public class LoginRequestDTO
    {
        public LoginRequestDTO(string user, string password)
        {
            User = user;
            Password = password;
        }

        public string User { get; private set; }
        public string Password { get; private set; }
    }
}
