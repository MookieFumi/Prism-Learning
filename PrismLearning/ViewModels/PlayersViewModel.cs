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
using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrismLearning.ViewModels
{
    public class PlayersViewModel : ViewModelBase
    {
        private readonly IPlayersService _playersService;

        private bool _isLoading = false;
        private ObservableCollection<PlayerDTO> _players;
        private PlayerDTO _selectedPlayer;

        public PlayersViewModel(INavigationService navigationService, IPageDialogService dialogService, IPlayersService playersService) : base(navigationService, dialogService)
        {
            _playersService = playersService;

            Title = "Players";

            NavigateToDetailCommand = new Command(async () =>
            {
                await NavigateToDetail();
            });

        }
        public ICommand NavigateToDetailCommand { get; private set; }

        #region Properties

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

        #endregion

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            IsLoading = true;
            if (parameters.Count > 0)
            {
                var team = parameters.GetValue<TeamDTO>("team");
                Players = new ObservableCollection<PlayerDTO>(await _playersService.GetPlayers(team.Acronym));
            }
            else
            {
                Players = new ObservableCollection<PlayerDTO>(await _playersService.GetPlayers());
            }
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

        private async Task NavigateToDetail()
        {
            Dictionary<string, string> properties = new Dictionary<string, string>
            {
                { "Player", Newtonsoft.Json.JsonConvert.SerializeObject(_selectedPlayer) }
            };
            Analytics.TrackEvent("Navigate To Detail View", properties);

            var parameters = new NavigationParameters
            {
                { "player", _selectedPlayer }
            };
            await NavigationService.NavigateAsync(nameof(PlayerDetailView), TransitionType.Scale, parameters);
        }
    }
}
