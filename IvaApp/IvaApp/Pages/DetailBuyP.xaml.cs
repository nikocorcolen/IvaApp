using System;
using System.Collections.Generic;
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
            nameEntry.Text = bs.ProductName;
            facturaEntry.Text = bs.Factura.ToString();
            dateEntry.Date = bs.Date;
            priceEntry.Text = bs.Price.ToString();

        }
    }
}
