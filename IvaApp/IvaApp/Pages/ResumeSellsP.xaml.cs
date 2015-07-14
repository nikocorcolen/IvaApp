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
            desdeDate.Date = Utilities.GetStartMonth();
            hastaDate.Date = Utilities.GetFinishMonth();

            //toolBar();
            using (var database = new BuyAndSellDatabase())
            {
                sellListView.ItemsSource = database.GetSells(Utilities.GetStartMonth(), Utilities.GetFinishMonth());
            }
            nuevo.Clicked += nuevo_Clicked;
            sellListView.ItemSelected += sellListView_ItemSelected;
            actualizar.Clicked += actualizar_Clicked;
        }

        public ResumeSellsP(List<BuyAndSell> itemSource, DateTime desde, DateTime hasta)
        {
            InitializeComponent();
            Title = "Resumen de Ventas";

            string monthName = Utilities.GetMonthName(DateTime.Now);
            int year = DateTime.Now.Year;
            desdeDate.Date = desde.Date;
            hastaDate.Date = hasta.Date;

            sellListView.ItemsSource = itemSource;

            sellListView.ItemSelected += sellListView_ItemSelected;
            nuevo.Clicked += nuevo_Clicked;
            actualizar.Clicked += actualizar_Clicked;
        }

        public async void actualizar_Clicked(object sender, EventArgs e)
        {
            using (var database = new BuyAndSellDatabase())
            {
                //buyListView.ItemsSource = database.GetBuys(desdeDate.Date, hastaDate.Date);
                await Navigation.PushAsync(new ResumeBuysP(database.GetSells(desdeDate.Date, hastaDate.Date), desdeDate.Date, hastaDate.Date));
                Navigation.RemovePage(this);
            }
        }

        public async void nuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrySellP());
            Navigation.RemovePage(this);
        }

        public async void sellListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BuyAndSell bs = (BuyAndSell)e.SelectedItem;
            await Navigation.PushAsync(new DetailSellP(bs));
            Navigation.RemovePage(this);
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
