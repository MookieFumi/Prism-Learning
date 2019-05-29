using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using PrismLearning.Controls;
using PrismLearning.DomainService.Abstractions;
using PrismLearning.DomainService.Abstractions.DTO;
using PrismLearning.ViewModels.Base;
using PrismLearning.Views;
using PrismLearning.Extensions;

namespace PrismLearning.ViewModels
{
    public class PlayersViewModel : ViewModelBase
    {
        private readonly ITeamsService _teamService;
        private readonly IPlayersService _playersService;

        private bool _isLoading = false;
        private ObservableCollection<PlayerDTO> _players;
        private PlayerDTO _selectedPlayer;
        private ObservableCollection<TeamDTO> _teams;

        public PlayersViewModel(INavigationService navigationService, IPageDialogService dialogService, ITeamsService teamService, IPlayersService playersService) : base(navigationService, dialogService)
        {
            _teamService = teamService;
            _playersService = playersService;

            Title = "Players";

            NavigateToDetailCommand = new DelegateCommand(async () => await NavigateToDetail());
        }

        public DelegateCommand NavigateToDetailCommand { get; private set; }

        #region Properties

        private async Task NavigateToDetail()
        {
            var parameters = new NavigationParameters
            {
                { "player", _selectedPlayer }
            };

            await NavigationService.NavigateAsync(nameof(PlayerDetailView), TransitionType.Scale, parameters);
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public ObservableCollection<PlayerDTO> Players
        {
            get { return _players; }
            set { SetProperty(ref _players, value); }
        }

        public PlayerDTO SelectedPlayer
        {
            get { return _selectedPlayer; }
            set { SetProperty(ref _selectedPlayer, value); }
        }

        public ObservableCollection<TeamDTO> Teams
        {
            get { return _teams; }
            set { SetProperty(ref _teams, value); }
        }

        #endregion

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            IsLoading = true;
            //await Task.Delay(2000);
            Players = new ObservableCollection<PlayerDTO>(await _playersService.GetPlayers());
            IsLoading = false;
            base.OnNavigatingTo(parameters);
        }

        public override async void OnResume()
        {
            base.OnResume();
            IsLoading = true;
            Players = new ObservableCollection<PlayerDTO>(await _playersService.GetPlayers());
            IsLoading = false;
        }

        public override void OnSleep()
        {
            base.OnSleep();
            Players.Clear();
        }
    }
}
