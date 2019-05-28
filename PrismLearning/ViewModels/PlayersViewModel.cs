using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Plugin.SharedTransitions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using PrismLearning.DomainService.Abstractions;
using PrismLearning.DomainService.Abstractions.DTO;
using PrismLearning.ViewModels.Base;
using PrismLearning.Views;

namespace PrismLearning.ViewModels
{
    public class PlayersViewModel : ViewModelBase
    {
        private ObservableCollection<PlayerDTO> _players;

        private readonly ITeamsService _teamService;
        private readonly IPlayersService _playersService;
        private bool _isLoading = false;
        private PlayerDTO _selectedPlayer;


        public PlayersViewModel(INavigationService navigationService, IPageDialogService dialogService, ITeamsService teamService, IPlayersService playersService) : base(navigationService, dialogService)
        {
            _teamService = teamService;
            _playersService = playersService;

            Title = "Players";

            NavigateToDetailCommand = new DelegateCommand(async () => await NavigateToDetail());
        }

        private async Task NavigateToDetail()
        {
            var navigationParameters = new NavigationParameters
            {
                { "player", _selectedPlayer }
            };

            await NavigationService.NavigateAsync(nameof(PlayerDetailView), navigationParameters);
        }

        public DelegateCommand NavigateToDetailCommand { get; private set; }

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

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

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
