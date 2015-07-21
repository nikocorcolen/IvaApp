using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class SimulateBuyAndSell : ContentPage
    {
        private static string textIva = "IVA Total: ";
        private static double ivaActual = 0;
        private static double ivaParcial = 0;

        public SimulateBuyAndSell()
        {
            InitializeComponent();
            Title = "Simular IVA";

            using (var databse = new BuyAndSellDatabase())
            {
                DateTime mesActualI = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime mesActualF = mesActualI.AddMonths(1).AddDays(-1);

                string[] temp = databse.GetIvaMes(mesActualI, mesActualF).Split('+');
                double credito = Double.Parse(temp[0]);
                double debito = Double.Parse(temp[1]);
                ivaActual = credito - debito;
                ivaParcial = ivaActual;

            }
            valorIvaActual.Text = textIva + Utilities.GetIva(ivaActual);

            buttonActualizar.Clicked += buttonActualizar_Clicked;

        }

        void buttonActualizar_Clicked(object sender, EventArgs e)
        {
            double valorVenta;
            double valorCompra;

            if (String.IsNullOrEmpty(valorVentaEntry.Text))
            {
                valorVenta = 0;
            }
            else
            {
                valorVenta = double.Parse(valorVentaEntry.Text);
            }

            if (String.IsNullOrEmpty(valorCompraEntry.Text))
            {
                valorCompra = 0;
            }
            else
            {
                valorCompra = double.Parse(valorCompraEntry.Text);
            }

            ivaParcial = ivaActual + valorVenta - valorCompra;
            valorIvaActual.Text = textIva + Utilities.GetIva(ivaParcial);
        }
    }
}

