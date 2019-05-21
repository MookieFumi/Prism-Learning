using System.Collections.ObjectModel;
using Prism.Navigation;
using Prism.Services;
using PrismLearning.Services;
using PrismLearning.Services.DTO;
using PrismLearning.ViewModels.Base;

namespace PrismLearning.ViewModels
{
    public class PlayersViewModel : ViewModelBase
    {
        private ObservableCollection<PlayerDTO> _players;

        private readonly ITeamsService _teamService;
        private readonly IPlayersService _playersService;

        public PlayersViewModel(INavigationService navigationService, IPageDialogService dialogService, ITeamsService teamService, IPlayersService playersService) : base(navigationService, dialogService)
        {
            _teamService = teamService;
            _playersService = playersService;

            Title = "Players";
        }

        public ObservableCollection<PlayerDTO> Players
        {
            get { return _players; }
            set { SetProperty(ref _players, value); }
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            Players = new ObservableCollection<PlayerDTO>(await _playersService.GetPlayers());
            base.OnNavigatingTo(parameters);
        }
    }
}
