using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IvaApp
{
    public partial class RegistryBuy : ContentPage
    {
        public RegistryBuy()
        {
            InitializeComponent();
            Title = "Compra y venta";
            okButton.Clicked += okButton_Clicked;
        }

        public async void okButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(amountEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar un monto", "Aceptar");
                amountEntry.Focus();
            }
            else
            {
                int facturaTemp = 0;
                if (!string.IsNullOrEmpty(facturaEntry.Text))
                {
                    facturaTemp = Int32.Parse(facturaEntry.Text);
                }
                BuySell bs = new BuySell
                {
                    
                    Amount = Int32.Parse(amountEntry.Text),
                    Date = dateEntry.Date,
                    isBuy = true,
                    Name = nameEntry.Text,
                    Factura = facturaTemp,
                    Description = descriptionEntry.Text
                };
                using (var databse = new BuySellDatabase())
                {
                    databse.InsertBuySell(bs);
                    await Navigation.PushAsync(new BuySellPage());
                }
            }
        }
    }
}
