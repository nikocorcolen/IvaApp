using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class RegistryBuyP : ContentPage
    {
        Page page;
        public RegistryBuyP(Page page)
        {
            InitializeComponent();
            this.page = page;
            Title = "Nueva Compra";
            okButton.Clicked += okButton_Clicked;
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
                    isBuy = true,
                    ProductName = nameEntry.Text.Replace("\n", ""),
                    Factura = facturaTemp,
                    User = Utilities.usuario
                };
                using (var databse = new BuyAndSellDatabase())
                {
                    databse.InsertBuySell(bs);
                    //limpia los campos
                    facturaEntry.Text = "";
                    nameEntry.Text = "";
                    priceEntry.Text = "";
                    await Navigation.PushAsync(new ResumeBuysP());
                    Navigation.RemovePage(page);
                    Navigation.RemovePage(this);

                }
            }
        }
    }
}
