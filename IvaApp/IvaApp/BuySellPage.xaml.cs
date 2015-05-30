using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp
{
    public partial class BuySellPage : ContentPage
    {
        private static string textIva = "IVA del mes: ";

        public BuySellPage()
        {
            InitializeComponent();
            Title = "Compra y Venta";
            using(var databse = new BuySellDatabase())
            {
                ivaLabel.Text = textIva + "$400";// + el iva que se obtenga de la consulta a la bd
            }

            registryBuyButton.Clicked += registryBuyButton_Clicked;
            registrySellButton.Clicked += registrySellButton_Clicked;
            simulateBuyButton.Clicked += simulateBuyButton_Clicked;
            simulateSellButton.Clicked += simulateSellButton_Clicked;
        }

        public async void simulateSellButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Simular ventas", "Aceptar");
        }

        public async void simulateBuyButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Simular compras", "Aceptar");
        }

        public async void registrySellButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Registro de ventas", "Aceptar");
        }

        public async void registryBuyButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Registro de compras", "Aceptar");
        }
    }
}
