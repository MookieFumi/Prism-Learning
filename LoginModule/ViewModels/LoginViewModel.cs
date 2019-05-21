using System;
using System.Threading.Tasks;
using LoginModule.Services;
using LoginModule.Services.Model;
using LoginModule.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace LoginModule.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _user = string.Empty;
        private string _password = string.Empty;
        private readonly ILoginService _loginService;

        public LoginViewModel(INavigationService navigationService, IPageDialogService dialogService, ILoginService loginService) : base(navigationService, dialogService)
        {
            _loginService = loginService;

            LoginCommand = new DelegateCommand(async () => await Login(), LoginCanExecute)
                                    .ObservesProperty(() => User)
                                    .ObservesProperty(() => Password);

#if DEBUG
            User = "user@company.com";
            Password = "theP@ssword";
#endif
        }

        public DelegateCommand LoginCommand { get; private set; }

        public string User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private async Task Login()
        {
            try
            {
                var response = await _loginService.Login(new LoginRequestDTO(User, Password));
                if (response.User == User)
                {
                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/MainView");
                }
            }
            catch (Exception ex)
            {
                await DisplayError(ex.Message);
            }
        }

        private bool LoginCanExecute()
        {
            return !string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password);
        }
    }
}
