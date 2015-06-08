using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class ResumeSellsP : ContentPage
    {
        public ResumeSellsP()
        {
            InitializeComponent();
            Title = "Resumen de Ventas";

            string monthName = Utilities.GetMonthName(DateTime.Now);
            int year = DateTime.Now.Year;

            TituloResumen.Text = "Ventas " + monthName + " de " + year;

            toolBar();
            using (var database = new BuyAndSellDatabase())
            {
                sellListView.ItemsSource = database.GetSells(Utilities.GetStartMonth(), Utilities.GetFinishMonth());
            }

            sellListView.ItemSelected += sellListView_ItemSelected;
        }

        void sellListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BuyAndSell bs = (BuyAndSell)e.SelectedItem;
            this.Navigation.PushAsync(new DetailSellP(bs));
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
