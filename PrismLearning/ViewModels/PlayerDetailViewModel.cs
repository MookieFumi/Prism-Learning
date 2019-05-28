using LoginModule.ViewModels.Base;
using Prism.Navigation;
using Prism.Services;
using PrismLearning.DomainService.Abstractions.DTO;

namespace PrismLearning.ViewModels
{
    public class PlayerDetailViewModel : ViewModelBase
    {
        private PlayerDTO _player;
        public PlayerDetailViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        public PlayerDTO Player
        {
            get => _player;
            set
            {
                SetProperty(ref _player, value);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Player = parameters.GetValue<PlayerDTO>("player");
            base.OnNavigatedTo(parameters);
        }
    }
}
