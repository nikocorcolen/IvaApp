using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class ResumeBuysP : ContentPage
    {
        public ResumeBuysP()
        {
            InitializeComponent();
            Title = "Resumen de Compras";

            string monthName = Utilities.GetMonthName(DateTime.Now);
            int year = DateTime.Now.Year;

            TituloResumen.Text = "Compras " + monthName + " de " + year;

            toolBar();
            using (var database = new BuyAndSellDatabase())
            {
                buyListView.ItemsSource = database.GetBuys(Utilities.GetStartMonth(), Utilities.GetFinishMonth());
            }

            buyListView.ItemSelected += buyListView_ItemSelected;
        }

        void buyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BuyAndSell bs = (BuyAndSell)e.SelectedItem;
            this.Navigation.PushAsync(new DetailBuyP(bs));
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
