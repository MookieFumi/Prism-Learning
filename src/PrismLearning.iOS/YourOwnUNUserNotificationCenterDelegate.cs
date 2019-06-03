using System;
using Microsoft.AppCenter.Push;
using UserNotifications;

namespace PrismLearning.iOS
{
    public class YourOwnUNUserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        // This is a property that it is exposed so it can be used elsewhere.
        public bool didReceiveNotificationInForeground { get; set; }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            // Do something, e.g. set a Boolean property to track the foreground state.
            this.didReceiveNotificationInForeground = true;

            // This callback overrides the system default behavior, so MSPush callback should be proxied manually.
            Push.DidReceiveRemoteNotification(notification.Request.Content.UserInfo);

            // Complete handling the notification.
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            if (response.IsDefaultAction)
            {
                // User tapped on notification
            }

            // This callback overrides the system default behavior, so MSPush callback should be proxied manually.
            Push.DidReceiveRemoteNotification(response.Notification.Request.Content.UserInfo);

            // Complete handling the notification.
            completionHandler();
        }
    }
}
