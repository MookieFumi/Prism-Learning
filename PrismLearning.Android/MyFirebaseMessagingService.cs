using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Firebase.Messaging;

namespace PrismLearning.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            if (message.GetNotification() != null)
            {
                //These is how most messages will be received
                SendNotification(message);
            }
        }


        private void SendNotification(RemoteMessage message)
        {
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                var channel = new NotificationChannel("default", "Channel Name", NotificationImportance.High);
                notificationManager.CreateNotificationChannel(channel);
            }

            var random = new Random();
            var id = random.Next(9999 - 1000) + 1000;
            var notificationIntent = new Intent(this, typeof(MainActivity));
            notificationIntent.AddFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);
            notificationIntent.PutExtra("Notification", Newtonsoft.Json.JsonConvert.SerializeObject(message));

            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.OneShot);

            var notification = message.GetNotification();
            var builder = new NotificationCompat.Builder(this, "default")
                .SetContentIntent(pendingIntent)
                .SetSmallIcon(Resource.Mipmap.icon)
                .SetContentTitle(notification.Title ?? "My custom title")
                .SetContentText(notification.Body)
                .SetAutoCancel(false);

            var pushNotification = builder.Build();
            //pushNotification.Flags = NotificationFlags.AutoCancel;

            notificationManager.Notify(id, pushNotification);
        }
    }
}