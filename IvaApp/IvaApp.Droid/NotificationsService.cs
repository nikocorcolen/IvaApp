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

namespace IvaApp.Droid
{
    [Service]
    public class NotificationsService : Service
    {
        public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
        {
          // start a task here
          new Task (() => {
              new Task(() =>
              {
                  while (true)
                  {
                      if (DateTime.Now.ToString("dd/MM/yyyy").Equals(Utilities.GetFinishMonth().ToString("dd/MM/yyyy")))
                      {
                          string title = "Hoy temina el mes de " + Utilities.GetMonthName(DateTime.Now);
                          string contexto = "Siguiente mes " + Utilities.GetNextMonthName(DateTime.Now);
                          Notification.Builder builder = new Notification.Builder(Android.App.Application.Context)
                          .SetContentTitle(title)
                          .SetContentText(contexto)
                          .SetDefaults(NotificationDefaults.Sound)
                          .SetSmallIcon(Resource.Drawable.logoIvaApp);

                          builder.SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate);
                          // Build the notification:
                          Notification notification = builder.Build();

                          notification.Defaults |= NotificationDefaults.Vibrate;
                          

                          // Get the notification manager:
                          NotificationManager notificationManager =
                              Android.App.Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

                          // Publish the notification:
                          const int notificationId = 0;
                          notificationManager.Notify(notificationId, notification);
                      }
                      if (DateTime.Now.ToString("dd/MM/yyyy").Equals(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10).ToString("dd/MM/yyyy")))
                      {
                          string title1 = "Pagar IVA";
                          string contexto1 = "Quedan 2 días para pagar " + Utilities.GetPrevMonthName(DateTime.Now);
                          Notification.Builder builder1 = new Notification.Builder(Android.App.Application.Context)
                          .SetContentTitle(title1)
                          .SetContentText(contexto1)
                          .SetDefaults(NotificationDefaults.Sound)
                          .SetSmallIcon(Resource.Drawable.logoIvaAppBlanco);

                          builder1.SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate);

                          // Build the notification:
                          Notification notification1 = builder1.Build();
                          builder1.SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate);
                          // Get the notification manager:
                          NotificationManager notificationManager1 =
                              Android.App.Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

                          // Publish the notification:
                          const int notificationId1 = 0;
                          notificationManager1.Notify(notificationId1, notification1);
                      }
                      Thread.Sleep(18000000);
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