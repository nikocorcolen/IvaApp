﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = "Calculator App";

            signinButton.Clicked += signinButton_Clicked;
            signupButton.Clicked += signupButton_Clicked;
        }

        public async void signinButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar un usuario", "Aceptar");
                userEntry.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar una contraseña", "Aceptar");
                passwordEntry.Focus();
                return;
            }
            else
            {
                using (var database = new UserDatabase())
                {
                    User u = database.GetUsuario(userEntry.Text);

                    if (u == null)
                    {
                        await DisplayAlert("Error", "Usuario o contraseña incorrecta", "Aceptar");
                    }
                    else
                    {
                        await Navigation.PushAsync(new BuySellPage());
                    }
                }
            }
        }

        public async void signupButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistryPage());
        }
    }
}
