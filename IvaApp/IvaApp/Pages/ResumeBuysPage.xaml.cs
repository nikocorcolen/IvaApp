using IvaApp.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IvaApp
{
    public partial class BuyPage : ContentPage
    {
        public BuyPage()
        {
            InitializeComponent();
            Title = "Resumen de compras";
            toolBar();
            using (var database = new BuySellDatabase())
            {
                buyListView.ItemsSource = database.GetBuySell();
            }
        }

        private void toolBar()
        {
            ToolbarItems.Clear();
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Salir",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0,
                Command = new Command(() => DependencyService.Get<IClose>().Close_App())
            });
        }
    }
}
