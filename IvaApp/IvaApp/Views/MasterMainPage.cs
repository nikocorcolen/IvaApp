using IvaApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IvaApp
{
    class MasterMainPage : MasterDetailPage
    {
        public MasterMainPage()
        {
            Label header = new Label
            {
                Text = "Menú",
                FontSize = Font.SystemFontOfSize(30, FontAttributes.Bold).FontSize,
                HorizontalOptions = LayoutOptions.Center
            };

            Aplicacion[] opciones = 
            {
                new Aplicacion("Inicio"),
                new Aplicacion("Registrar Compra"),
                new Aplicacion("Registrar Venta"),
                new Aplicacion("Simular Compra"),
                new Aplicacion("Simular Venta"),
                new Aplicacion("Cerrar Sesión")
            };

            ListView listView = new ListView
            {
                ItemsSource = opciones
            };

            this.Master = new ContentPage
            {
                Title = "Menú de Opciones",
                Content = new StackLayout
                {
                    Children =
                    {
                        header,
                        listView
                    }
                }
            };

            this.Detail = new NavigationPage(new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
                }
            });
            /*
            listView.ItemSelected += (sender, args) =>
            {
                this.Detail.BindingContext = args.SelectedItem;

                this.IsPresented = false;
            };*/

            listView.ItemSelected += (sender, args) =>
                {
                    NavigationPage detalle = null;

                    var item = args.SelectedItem as Aplicacion;
                    switch (item.Nombre)
                    {
                        case "Inicio": detalle = new NavigationPage(new BuyAndSellP()); break;
                        case "Registrar Compra": detalle = new NavigationPage(new RegistryBuyP()); break;
                        case "Registrar Venta": detalle = new NavigationPage(new RegistrySellP()); break;
                        case "Cerrar Sesión": logout_session(); break;
                    }

                    if (detalle == null) return;
                    IsPresented = false;
                    this.Detail = detalle;
                };

            listView.SelectedItem = opciones[0];
            Master.Icon = "slideout.png";
            
        }

        public async void logout_session()
        {
            await Navigation.PushModalAsync(new MainPage());
        }
    }
}
