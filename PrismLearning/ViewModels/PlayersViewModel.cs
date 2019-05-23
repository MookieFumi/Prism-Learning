using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        private bool _isLoading = false;


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

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            IsLoading = true;
            await Task.Delay(2000);
            Players = new ObservableCollection<PlayerDTO>(await _playersService.GetPlayers("hou"));
            IsLoading = false;
            base.OnNavigatingTo(parameters);
        }
    }
}
