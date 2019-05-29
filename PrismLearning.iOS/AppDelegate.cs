using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;

namespace PrismLearning.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.SetFlags("Visual_Experimental", "CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            CachedImageRenderer.InitImageSourceHandler();

            LoadApplication(new App(new IOSInitializer()));

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
