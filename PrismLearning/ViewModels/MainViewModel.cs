using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using MonkeyCache;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using PrismLearning.DomainService.Abstractions;
using PrismLearning.DomainService.Abstractions.DTO;
using PrismLearning.ViewModels.Base;
using PrismLearning.Views;

namespace PrismLearning.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IBarrel _barrel;
        private readonly ITeamsService _teamService;

        private bool _isPanelVisible = false;
        private bool _isFullscreenLoading = false;
        private string _searchText;
        private ObservableCollection<TeamDTO> _teams;

        public MainViewModel(INavigationService navigationService, IPageDialogService dialogService, IBarrel barrel, ITeamsService teamService) : base(navigationService, dialogService)
        {
            Title = "Main view";

            _barrel = barrel;
            _teamService = teamService;

            PanelCommand = new DelegateCommand(() => IsPanelVisible = !IsPanelVisible);
            ShowLoadingCommand = new DelegateCommand(async () => await ShowLoading());
            GoToPlayersViewCommand = new DelegateCommand(async () => await GoToPlayersView());
            ClearCacheCommand = new DelegateCommand(() => _barrel.EmptyAll());
            CrashCommand = new DelegateCommand(async () => await Crash());
        }

        private async Task Crash()
        {
            try
            {
                throw new DivideByZeroException();
            }
            catch (Exception exception)
            {
                await HandleError(exception);
            }
        }


        public DelegateCommand PanelCommand { get; private set; }
        public DelegateCommand GoToPlayersViewCommand { get; private set; }
        public DelegateCommand ShowLoadingCommand { get; private set; }
        public DelegateCommand ClearCacheCommand { get; private set; }
        public DelegateCommand CrashCommand { get; private set; }

        #region Properties
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

        public ObservableCollection<TeamDTO> Teams
        {
            get { return _teams; }
            set { SetProperty(ref _teams, value); }
        }
        #endregion

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            await ShowLoading();
            Teams = new ObservableCollection<TeamDTO>(await _teamService.GetTeams());
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnSleep()
        {
            base.OnSleep();
        }

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
    }
}
