using FFImageLoading.Forms.Platform;
using Foundation;
using Microsoft.AppCenter.Push;
using UIKit;
using UserNotifications;

namespace PrismLearning.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private YourOwnUNUserNotificationCenterDelegate myOwnNotificationDelegate = null;

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

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                this.myOwnNotificationDelegate = new YourOwnUNUserNotificationCenterDelegate();
                UNUserNotificationCenter.Current.Delegate = this.myOwnNotificationDelegate;
            }

            Push.PushNotificationReceived += (sender, e) =>
            {
                if (this.myOwnNotificationDelegate.didReceiveNotificationInForeground)
                {
                    // Handle the push notification that was received while in foreground.
                }
                else
                {
                    // Handle the push notification that was received while in background.
                }

                // Reset the property for next notifications.
                this.myOwnNotificationDelegate.didReceiveNotificationInForeground = false;
            };

#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Push.RegisteredForRemoteNotifications(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Push.FailedToRegisterForRemoteNotifications(error);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, System.Action<UIBackgroundFetchResult> completionHandler)
        {
            var result = Push.DidReceiveRemoteNotification(userInfo);
            if (result)
            {
                completionHandler?.Invoke(UIBackgroundFetchResult.NewData);
            }
            else
            {
                completionHandler?.Invoke(UIBackgroundFetchResult.NoData);
            }
        }
    }
}
