using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class DetailBuyP : ContentPage
    {
        private BuyAndSell bsGlobal;
        public DetailBuyP(BuyAndSell bs)
        {
            InitializeComponent();
            bsGlobal = bs;
            Title = "Detalle";
            productName.Text = bsGlobal.ProductName;
            factura.Text = bsGlobal.Factura.ToString();
            dateProduct.Text = bsGlobal.Date.Month + "/" + bsGlobal.Date.Day + "/" + bsGlobal.Date.Year;

            double t = bsGlobal.Price;

            neto.Text = Utilities.GetNeto(t);
            iva.Text = Utilities.GetIva(t);
            total.Text = Utilities.GetTotal(t);

            eliminarButton.Clicked += eliminarButton_Clicked;

        }

        public async void eliminarButton_Clicked(object sender, EventArgs e)
        {
            using (var database = new BuyAndSellDatabase())
            {
                database.DeleteBuyAndSell(bsGlobal);
                await Navigation.PushAsync(new ResumeBuysP());
                Navigation.RemovePage(this);

            }
        }
    }
}
