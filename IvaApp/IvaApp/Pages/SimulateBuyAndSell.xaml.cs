﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class SimulateBuyAndSell : ContentPage
    {
        private static string textIva = "Iva Total: ";
        private static double ivaActual = 0;
        private static double ivaParcial = 0;

        public SimulateBuyAndSell()
        {
            InitializeComponent();
            Title = "Simular Iva";

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

            valorVentaEntry.TextChanged += valorEntry_TextChanged;
            valorCompraEntry.TextChanged += valorEntry_TextChanged;

        }

        public void valorEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            double valorVenta;
            double valorCompra;

            if (valorVentaEntry.Text == "")
            {
                valorVenta = 0;
            }
            else
            {
                valorVenta = double.Parse(valorVentaEntry.Text);
            }

            if (valorCompraEntry.Text == "")
            {
                valorCompra = 0;
            }
            else
            {
                valorCompra = double.Parse(valorCompraEntry.Text);
            }

            ivaParcial = ivaActual - valorVenta + valorCompra;
            valorIvaActual.Text = textIva + Utilities.GetIva(ivaParcial);
        }
    }
}

