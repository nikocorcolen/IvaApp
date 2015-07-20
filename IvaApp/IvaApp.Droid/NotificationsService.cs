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
using System.Threading.Tasks;
using System.Threading;
using EdSnider.Plugins;

namespace IvaApp.Droid
{
    [Service]
    public class NotificationsService : Service
    {
        //Notifier.Current.Show("You've got mail", "You have 793 unread messages!");
        public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
        {
          // start a task here
          new Task (() => {
            // long running code
              new Task(() =>
              {
                  while (true)
                  {
                      if (DateTime.Now == Utilities.GetFinishMonth())
                      {
                          string title = "Hoy temina el mes de " + Utilities.GetMonthName(DateTime.Now);
                          string contexto = "Desde mañana los registros se guardarán en el mes de " + Utilities.GetNextMonthName(DateTime.Now);
                          Notification.Builder builder = new Notification.Builder(Android.App.Application.Context)
                          .SetContentTitle(title)
                          .SetContentText(contexto)
                          .SetSmallIcon(Resource.Drawable.icon);

                          // Build the notification:
                          Notification notification = builder.Build();

                          // Get the notification manager:
                          NotificationManager notificationManager =
                              Android.App.Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

                          // Publish the notification:
                          const int notificationId = 0;
                          notificationManager.Notify(notificationId, notification);
                      }
                      Thread.Sleep(10000);
                  }
                  //StopSelf();
              }).Start();
          }).Start();
          return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            // cleanup code
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}