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

[assembly: Dependency(typeof(Close))]
namespace IvaApp.Droid
{
    class Close : IClose
    {
        public void Close_App()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}