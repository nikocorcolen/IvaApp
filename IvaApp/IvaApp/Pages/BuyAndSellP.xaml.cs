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

        public BuyAndSellP()
        {
            InitializeComponent();
            Title = "Compra y Venta";
            //toolBar();

            resumeBuyButton.Clicked += registryBuyButton_Clicked;
            resumeSellButton.Clicked += registrySellButton_Clicked;
            detailIvaButton.Clicked += detailIvaButton_Clicked;
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
                Text = "Ventas",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0,
                Command = new Command(() => Navigation.PushAsync(new ResumeSellsP()))
            });
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Salir",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0,
                Command = new Command(() => DependencyService.Get<IClose>().Close_App())
            });
        }

        public async void simulateBuyButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Simular compras", "Aceptar");
        }

        public async void registrySellButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResumeSellsP());
        }

        public async void registryBuyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResumeBuysP());
        }

        public async void detailIvaButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetailsIvaP());
        }
    }
}
