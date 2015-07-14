using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class Reportes : ContentPage
    {
        public Reportes()
        {
            InitializeComponent();
            Title = "Reportes Mensuales";
            desdeDate.Date = Utilities.GetStartMonth();
            hastaDate.Date = Utilities.GetFinishMonth();
            comprasMensuales.Clicked += comprasMensuales_Clicked;
            ventasMensuales.Clicked += ventasMensuales_Clicked;
        }

        async void ventasMensuales_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Enviando reporte, espere que le llege el correo para generar uno nuevo", "Aceptar");
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                using (var database = new BuyAndSellDatabase())
                {
                    List<BuyAndSell> a = database.GetSells(desdeDate.Date, hastaDate.Date);
                    DependencyService.Get<ISave>().SaveText(a);
                }
                using (var database1 = new UserDatabase())
                {
                    User u = database1.GetMail(Utilities.usuario);
                    String correo = u.Mail.ToString();
                    String tipo = "venta";
                    DependencyService.Get<IEmail>().Send_Email(correo, tipo);
                }
            });
        }

        async void comprasMensuales_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Enviando reporte, espere que le llege el correo para generar uno nuevo", "Aceptar");
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
    }
}
