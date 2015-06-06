using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using IvaApp.Droid;
using Android.Support.V4.App;

[assembly: Dependency(typeof(Notification_Android))]
namespace IvaApp.Droid
{
    class Notification_Android : INotification
    {
        public void Notification()
        {
            var context = Forms.Context;

            var resultIntent = new Intent(context, typeof(NotificationActivity));
            var stackBuilder = Android.App.TaskStackBuilder.Create(context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(NotificationActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            var resultPendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

            Notification.Builder builder = new Notification.Builder(context)
                            .SetAutoCancel(true)
                            .SetContentTitle("Button Clicked")
                            .SetContentText(String.Format("The button has been clicked"))
                            .SetContentIntent(resultPendingIntent);
            builder.SetVisibility(NotificationVisibility.Public);
            builder.SetCategory("CATEGORY_ALARM");

            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            Notification notification = builder.Build();
            notification.Defaults |= NotificationDefaults.Vibrate;
            notificationManager.Notify(0, notification);
        }
    }
}