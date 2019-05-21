using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace LoginModule.ViewModels.Base
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
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
    }
}
