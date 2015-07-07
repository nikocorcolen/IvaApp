using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class RegistrySellP : ContentPage
    {
        public RegistrySellP()
        {
            InitializeComponent();
            Title = "Nueva Venta";
            toolBar();
            okButton.Clicked += okButton_Clicked;
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
                Text = "Salir",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0,
                Command = new Command(() => DependencyService.Get<IClose>().Close_App())
            });
        }

        public async void okButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(priceEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar un monto", "Aceptar");
                priceEntry.Focus();
            }
            else
            {
                int facturaTemp = 0;
                if (!string.IsNullOrEmpty(facturaEntry.Text))
                {
                    facturaTemp = Int32.Parse(facturaEntry.Text);
                }
                BuyAndSell bs = new BuyAndSell
                {

                    Price = Double.Parse(priceEntry.Text),
                    Date = dateEntry.Date,
                    isBuy = false,
                    ProductName = nameEntry.Text,
                    Factura = facturaTemp,
                    Usuario = Utilities.usuario
                };
                using (var databse = new BuyAndSellDatabase())
                {
                    databse.InsertBuySell(bs);
                    //Limpia los campos
                    facturaEntry.Text = "";
                    priceEntry.Text = "";
                    nameEntry.Text = "";
                    await Navigation.PushAsync(new BuyAndSellP());
                }
            }
        }
    }
}
