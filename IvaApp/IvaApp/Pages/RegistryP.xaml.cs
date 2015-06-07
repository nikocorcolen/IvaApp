using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class RegistryP : ContentPage
    {
        public RegistryP()
        {
            InitializeComponent();
            Title = "Crear Usuario";
            toolBar();
            registryButton.Clicked += registryButton_Clicked;
        }

        private void toolBar()
        {
            ToolbarItems.Clear();
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Salir",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0,
                Command = new Command(() => DependencyService.Get<IClose>().Close_App())
            });
        }

        public async void registryButton_Clicked(object sender, EventArgs e)
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
                User u = new User
                {
                    Username = userEntry.Text,
                    Password = passwordEntry.Text
                };
                using (var database = new UserDatabase())
                {
                    if (database.InsertUser(u))
                    {
                        await DisplayAlert("Exito", "Usuario creado correctamente", "Aceptar");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Usuario creado incorrectamente", "Aceptar");
                    }
                }
                //Return to the MainPage
                await Navigation.PushAsync(new MainPage());
            }
        }
    }
}
