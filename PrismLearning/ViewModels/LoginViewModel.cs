using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using PrismLearning.Services;
using PrismLearning.ViewModels.Base;
using PrismLearning.Views;
using Xamarin.Forms;

namespace PrismLearning.ViewModels
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
                var response = await _loginService.Login(new LoginRequest(User, Password));
                if (response.User == User)
                {
                    await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainView)}");
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
