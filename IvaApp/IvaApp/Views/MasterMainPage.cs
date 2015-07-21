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
                Text = "Opciones",
                FontSize = Font.SystemFontOfSize(30, FontAttributes.Bold).FontSize,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center
            };

            Aplicacion[] opciones = 
            {
                new Aplicacion("Inicio"),
                new Aplicacion("Simular"),
                new Aplicacion("Reportes"),
                new Aplicacion("Acerca de"),
                new Aplicacion("Salir")
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
                        case "Simular": detalle = new NavigationPage(new SimulateBuyAndSell()); break;
                        case "Reportes": detalle = new NavigationPage(new Reportes()); break;
                        case "Acerca de": detalle = new NavigationPage(new AcercaDe()); break;
                        case "Salir": logout_session(); break;
                    }

                    if (detalle == null) return;
                    IsPresented = false;
                    this.Detail = detalle;
                };

            listView.SelectedItem = opciones[0];
            Master.Icon = "slideout.png";
            
        }

        public void logout_session()
        {
            DependencyService.Get<IClose>().Close_App();
        }
    }
}
