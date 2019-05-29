using System.Threading.Tasks;
using Prism.Navigation;
using PrismLearning.Controls;

namespace PrismLearning.Extensions
{
    public static class NavigationServiceExtension
    {
        public static async Task NavigateAsync(this INavigationService navigationService, string name, TransitionType transitionType = TransitionType.Default, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
        {
            (Prism.PrismApplicationBase.Current.MainPage as TransitionNavigationPage).TransitionType = transitionType;

            await navigationService.NavigateAsync(name, parameters, useModalNavigation, animated);
        }
    }
}
