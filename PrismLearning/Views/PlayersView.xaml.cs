using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace PrismLearning.Views
{
    public partial class PlayersView : ContentPage
    {
        public PlayersView()
        {
            InitializeComponent();
            SharedTransitionNavigationPage.SetBackgroundAnimation(this, BackgroundAnimation.Fade);
            SharedTransitionNavigationPage.SetSharedTransitionDuration(this, 500);
        }
    }
}