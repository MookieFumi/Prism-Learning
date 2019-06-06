using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Auth;
using MonkeyCache;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using PrismLearning.Controls;
using PrismLearning.DomainService.Abstractions;
using PrismLearning.DomainService.Abstractions.DTO;
using PrismLearning.Extensions;
using PrismLearning.ViewModels.Base;
using PrismLearning.Views;

namespace PrismLearning.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IBarrel _barrel;
        private readonly ITeamsService _teamService;

        private bool _isPanelVisible = false;
        private bool _isFullscreenLoading = false;
        private string _searchText;
        private ObservableCollection<TeamDTO> _teams;
        private TeamDTO _selectedTeam;

        public MainViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator eventAggregator, IBarrel barrel, ITeamsService teamService) : base(navigationService, dialogService)
        {
            Title = "Main view";

            _barrel = barrel;
            _teamService = teamService;
            _eventAggregator = eventAggregator;

            PanelCommand = new DelegateCommand(() => IsPanelVisible = !IsPanelVisible);
            ShowLoadingCommand = new DelegateCommand(async () => await ShowLoading());
            GoToPlayersViewCommand = new DelegateCommand(async () => await GoToPlayersView());
            ClearCacheCommand = new DelegateCommand(() => _barrel.EmptyAll());
            CrashCommand = new DelegateCommand(async () => await Crash());
            NavigateToDetailCommand = new DelegateCommand(async () => await NavigateToDetail());
            SignInCommand = new DelegateCommand(async () => await SignIn());
            SignOutCommand = new DelegateCommand(async () => await SignOut());

            _eventAggregator.GetEvent<MyEvent>()?.Subscribe(async () =>
            {
                await DialogService.DisplayAlertAsync("Notification", "Show notification", "Ok");
            });
        }



        public DelegateCommand PanelCommand { get; private set; }
        public DelegateCommand GoToPlayersViewCommand { get; private set; }
        public DelegateCommand ShowLoadingCommand { get; private set; }
        public DelegateCommand ClearCacheCommand { get; private set; }
        public DelegateCommand CrashCommand { get; private set; }
        public DelegateCommand NavigateToDetailCommand { get; private set; }
        public DelegateCommand SignInCommand { get; private set; }
        public DelegateCommand SignOutCommand { get; private set; }

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

        public TeamDTO SelectedTeam
        {
            get { return _selectedTeam; }
            set { SetProperty(ref _selectedTeam, value); }
        }
        #endregion

        public override void Destroy()
        {
            _eventAggregator.GetEvent<MyEvent>()?.Unsubscribe(() => { });
            base.Destroy();
        }

        private async Task NavigateToDetail()
        {
            IsPanelVisible = false;
            Dictionary<string, string> properties = new Dictionary<string, string>
            {
                { "Team", Newtonsoft.Json.JsonConvert.SerializeObject(_selectedTeam) }
            };
            Analytics.TrackEvent("Navigate To Detail View", properties);

            var parameters = new NavigationParameters
            {
                { "team", _selectedTeam }
            };
            await NavigationService.NavigateAsync(nameof(PlayersView), TransitionType.Scale, parameters);
        }

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

        private async Task SignIn()
        {
            try
            {
                // Sign-in succeeded.
                UserInformation userInfo = await Auth.SignInAsync();
                if (userInfo != null)
                {
                    await DialogService.DisplayAlertAsync("Info", $"Login successful! \nAccountId: {userInfo.AccountId} ", "Ok");
                }
            }
            catch (Exception exception)
            {
                await HandleError(exception);
            }
        }

        private async Task SignOut()
        {
            try
            {
                Auth.SignOut();
                await DialogService.DisplayAlertAsync("Info", "Logout successful!", "Ok");
            }
            catch (Exception exception)
            {
                await HandleError(exception);
            }
        }


    }
}
