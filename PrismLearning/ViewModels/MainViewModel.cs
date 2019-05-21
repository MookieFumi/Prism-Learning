using System;
using System.Threading.Tasks;
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

        public MainViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Main view";

            PanelCommand = new DelegateCommand(() => IsPanelVisible = !IsPanelVisible);

            GoToPlayersViewCommand = new DelegateCommand(async () => await GoToPlayersView());
        }

        public bool IsPanelVisible
        {
            get { return _isPanelVisible; }
            set { SetProperty(ref _isPanelVisible, value); }
        }

        public DelegateCommand PanelCommand { get; private set; }
        public DelegateCommand GoToPlayersViewCommand { get; private set; }

        private async Task GoToPlayersView()
        {
            await NavigationService.NavigateAsync(nameof(PlayersView));
        }
    }
}
