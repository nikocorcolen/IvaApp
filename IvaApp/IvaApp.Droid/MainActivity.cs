using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace IvaApp.Droid
{
    //[Activity(Label = "IvaApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    [Activity(
        Label = "IVA App",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        Theme = "@style/CustomTheme")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            //StartService(new Intent(this, typeof(NotificationsService)));
            //Android.Content.Intent intent = new Android.Content.Intent("com.my.command");
            //StartService(intent);
            this.StartService(new Intent(this, typeof(NotificationsService)));
            LoadApplication(new App());

        }
    }
}

