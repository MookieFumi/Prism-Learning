using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace PrismLearning.Views
{
    public partial class CustomNavigationPage : SharedTransitionNavigationPage
    {
        public CustomNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}
