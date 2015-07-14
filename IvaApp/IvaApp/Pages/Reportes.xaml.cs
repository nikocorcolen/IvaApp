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
            comprasMensuales.Clicked += comprasMensuales_Clicked;
            ventasMensuales.Clicked += ventasMensuales_Clicked;
        }

        async void ventasMensuales_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Enviar Ventas", "Aceptar");
            using (var database = new BuyAndSellDatabase())
            {
                List<BuyAndSell> a = database.GetSells(Utilities.GetStartMonth(), Utilities.GetFinishMonth());
                DependencyService.Get<ISave>().SaveText(a);
            }
            using (var database1 = new UserDatabase())
            {
                String correo = database1.GetMail(Utilities.usuario);
                String tipo = "venta";
                DependencyService.Get<IEmail>().Send_Email(correo, tipo);
            }
        }

        async void comprasMensuales_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Enviar Compras", "Aceptar");
            using (var database = new BuyAndSellDatabase())
            {
                List<BuyAndSell> a = database.GetBuys(Utilities.GetStartMonth(), Utilities.GetFinishMonth());
                DependencyService.Get<ISave>().SaveText(a);
            }
            using (var database1 = new UserDatabase())
            {
                String correo = database1.GetMail(Utilities.usuario);
                String tipo = "compra";
                DependencyService.Get<IEmail>().Send_Email(correo, tipo);
            }
        }
    }
}
