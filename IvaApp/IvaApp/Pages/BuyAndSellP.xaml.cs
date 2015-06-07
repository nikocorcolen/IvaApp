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
            toolBar();
            using(var databse = new BuyAndSellDatabase())
            {
                ivaLabel.Text = textIva + "$400";// + el iva que se obtenga de la consulta a la bd
            }

            registryBuyButton.Clicked += registryBuyButton_Clicked;
            registrySellButton.Clicked += registrySellButton_Clicked;
            simulateBuyButton.Clicked += simulateBuyButton_Clicked;
            simulateSellButton.Clicked += simulateSellButton_Clicked;
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
            await Navigation.PushAsync(new ResumeBuysP());
        }
    }
}
