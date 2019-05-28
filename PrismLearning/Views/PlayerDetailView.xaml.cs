using System;
using System.Collections.Generic;
using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace PrismLearning.Views
{
    public partial class PlayerDetailView : ContentPage
    {
        public PlayerDetailView()
        {
            InitializeComponent();
            SharedTransitionNavigationPage.SetSharedTransitionDuration(this, 500);
        }
    }
}
