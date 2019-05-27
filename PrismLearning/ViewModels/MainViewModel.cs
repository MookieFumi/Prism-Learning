using System.Threading.Tasks;
using MonkeyCache;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using PrismLearning.ViewModels.Base;
using PrismLearning.Views;

namespace PrismLearning.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool _isPanelVisible = false;
        private bool _isFullscreenLoading = false;
        private string _searchText;
        private readonly IBarrel _barrel;

        public MainViewModel(INavigationService navigationService, IPageDialogService dialogService, IBarrel barrel) : base(navigationService, dialogService)
        {
            Title = "Main view";

            _barrel = barrel;

            PanelCommand = new DelegateCommand(() => IsPanelVisible = !IsPanelVisible);
            ShowLoadingCommand = new DelegateCommand(async () => await ShowLoading());
            GoToPlayersViewCommand = new DelegateCommand(async () => await GoToPlayersView());
            ClearCacheCommand = new DelegateCommand(() => _barrel.EmptyAll());
        }

        public bool IsPanelVisible
        {
            get { return _isPanelVisible; }
            set { SetProperty(ref _isPanelVisible, value); }
        }

        public bool IsFullscreenLoading
        {
            get { return _isFullscreenLoading; }
            set { SetProperty(ref _isFullscreenLoading, value); }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public DelegateCommand PanelCommand { get; private set; }
        public DelegateCommand GoToPlayersViewCommand { get; private set; }
        public DelegateCommand ShowLoadingCommand { get; private set; }
        public DelegateCommand ClearCacheCommand { get; private set; }

        private async Task GoToPlayersView()
        {
            await NavigationService.NavigateAsync(nameof(PlayersView));
        }

        private async Task ShowLoading()
        {
            IsFullscreenLoading = true;
            await Task.Delay(2000);
            IsFullscreenLoading = false;
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            await ShowLoading();
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnSleep()
        {
            base.OnSleep();
        }
    }
}
