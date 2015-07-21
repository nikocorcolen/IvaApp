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
            desdeDate.Date = Utilities.GetStartMonth();
            hastaDate.Date = Utilities.GetFinishMonth();

            //toolBar();
            using (var database = new BuyAndSellDatabase())
            {
                buyListView.ItemsSource = database.GetBuys(Utilities.GetStartMonth(), Utilities.GetFinishMonth());
                List<BuyAndSell> a = database.GetBuys(Utilities.GetStartMonth(), Utilities.GetFinishMonth());
            }

            buyListView.ItemSelected += buyListView_ItemSelected;
            nuevo.Clicked += nuevo_Clicked;
            actualizar.Clicked += actualizar_Clicked;
            report.Clicked += report_Clicked;
        }

        async void report_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Enviando reporte, espere que le llegue el correo para generar uno nuevo", "Aceptar");
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                using (var database = new BuyAndSellDatabase())
                {
                    List<BuyAndSell> a = database.GetBuys(desdeDate.Date, hastaDate.Date);
                    DependencyService.Get<ISave>().SaveText(a);
                }
                using (var database1 = new UserDatabase())
                {
                    User u = database1.GetMail(Utilities.usuario);
                    String correo = u.Mail.ToString();
                    String tipo = "compra";
                    DependencyService.Get<IEmail>().Send_Email(correo, tipo);
                }
            });
        }

        public ResumeBuysP(List<BuyAndSell> itemSource, DateTime desde, DateTime hasta)
        {
            InitializeComponent();
            Title = "Resumen de Compras";

            string monthName = Utilities.GetMonthName(DateTime.Now);
            int year = DateTime.Now.Year;
            desdeDate.Date = desde.Date;
            hastaDate.Date = hasta.Date;

            buyListView.ItemsSource = itemSource;

            buyListView.ItemSelected += buyListView_ItemSelected;
            nuevo.Clicked += nuevo_Clicked;
            actualizar.Clicked += actualizar_Clicked;
        }

        public async void actualizar_Clicked(object sender, EventArgs e)
        {
            using (var database = new BuyAndSellDatabase())
            {
                //buyListView.ItemsSource = database.GetBuys(desdeDate.Date, hastaDate.Date);
                await Navigation.PushAsync(new ResumeBuysP(database.GetBuys(desdeDate.Date, hastaDate.Date), desdeDate.Date, hastaDate.Date));
                Navigation.RemovePage(this);
            }
        }

        public async void nuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistryBuyP(this));
            //Navigation.RemovePage(this);
        }

        public async void buyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BuyAndSell bs = (BuyAndSell)e.SelectedItem;
            await Navigation.PushAsync(new DetailBuyP(bs, this));
            //Navigation.RemovePage(this);
            
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
