using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace IvaApp
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            //MainPage = new MasterMainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        //public static Page GetMainPage(string page)
        //{
        //    if (page == "second")
        //    {
        //        return new NavigationPage(new RegistryBuy());
        //    }
        //    else
        //    {
        //        return new NavigationPage(new MainPage());
        //    }
        //}

        protected override void OnStart()
        {
            
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
