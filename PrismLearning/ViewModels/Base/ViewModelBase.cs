using System.Threading.Tasks;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace PrismLearning.ViewModels.Base
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IApplicationLifecycleAware
    {
        protected readonly INavigationService NavigationService;
        protected readonly IPageDialogService DialogService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
        }

        protected async Task DisplayError(string message)
        {
            await DialogService.DisplayAlertAsync("Error", message, "Ok");
        }

        public void Destroy()
        {
            //throw new NotImplementedException();
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public virtual void OnResume()
        {
            //throw new System.NotImplementedException();
        }

        public virtual void OnSleep()
        {
            //throw new System.NotImplementedException();
        }
    }
}
