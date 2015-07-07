using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class BuyAndSellP : ContentPage
    {
        private static string textIva = "IVA del mes: ";

        public BuyAndSellP()
        {
            InitializeComponent();
            Title = "Compra y Venta";
            //toolBar();
            using(var databse = new BuyAndSellDatabase())
            {
                string[] temp = databse.GetIvaMes(Utilities.GetStartMonth(), Utilities.GetFinishMonth()).Split('+');
                double credito = Double.Parse(temp[0]);
                double debito = Double.Parse(temp[1]);

                if (debito - credito < 0)
                {
                    ivaLabel.TextColor = Color.Green;
                }
                else if (debito - credito > 0)
                {
                    ivaLabel.TextColor = Color.Red;
                }
                else
                {
                    ivaLabel.TextColor = Color.White;
                }

                ivaLabel.Text = textIva + Utilities.GetIva(debito - credito);
            }

            registryBuyButton.Clicked += registryBuyButton_Clicked;
            registrySellButton.Clicked += registrySellButton_Clicked;
            simulateBuyButton.Clicked += simulateBuyButton_Clicked;
            ivaLabel.Clicked += ivaLabel_Clicked;
        }

        private void toolBar()
        {
            ToolbarItems.Clear();
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Compras",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0,
                Command = new Command(() => Navigation.PushAsync(new ResumeBuysP()))
            });
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Ventas",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0,
                Command = new Command(() => Navigation.PushAsync(new ResumeSellsP()))
            });
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Salir",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0,
                Command = new Command(() => DependencyService.Get<IClose>().Close_App())
            });
        }

        public async void simulateBuyButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Simular compras", "Aceptar");
            using (var database = new BuyAndSellDatabase())
            {
                List<BuyAndSell> a = database.GetBuys(Utilities.GetStartMonth(), Utilities.GetFinishMonth(), Utilities.usuario);
                DependencyService.Get<ISave>().SaveText(a);
            }
            using (var database1 = new UserDatabase())
            {
                String correo = database1.GetMail(Utilities.usuario);
                DependencyService.Get<IEmail>().Send_Email(correo);
            }
        }

        public async void registrySellButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrySellP());
        }

        public async void registryBuyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistryBuyP());
        }

        public async void ivaLabel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetailsIvaP());
        }
    }
}
