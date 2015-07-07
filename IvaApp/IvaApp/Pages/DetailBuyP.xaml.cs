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
        public DetailBuyP(BuyAndSell bs)
        {
            InitializeComponent();
            Title = "Detalle";
            productName.Text = bs.ProductName;
            factura.Text = bs.Factura.ToString();
            dateProduct.Text = bs.Date.Month + "/" + bs.Date.Day + "/" + bs.Date.Year;

            double t = bs.Price;

            neto.Text = Utilities.GetNeto(t);
            iva.Text = Utilities.GetIva(t);
            total.Text = Utilities.GetTotal(t);

        }
    }
}
