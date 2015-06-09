using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace IvaApp.Pages
{
    public partial class DetailsIvaP : ContentPage
    {
        public DetailsIvaP()
        {
            InitializeComponent();
            Title = "Detalle";
            using(var databse = new BuyAndSellDatabase())
            {
                Double ivaMes1 = 0;
                Double ivaMes2 = 0;
                Double ivaMes3 = 0;
                Double ivaTotal = 0;

                //Mes -1
                DateTime mesActualI = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                mesActualI = mesActualI.AddMonths(-1);
                DateTime mesActualF = mesActualI.AddMonths(1).AddDays(-1);

                string[] temp = databse.GetIvaMes(mesActualI, mesActualF).Split('+');
                double credito = Double.Parse(temp[0]);
                double debito = Double.Parse(temp[1]);
                ivaMes1 = debito - credito;
                if (ivaMes1 < 0)
                {
                    iva1.TextColor = Color.Green;
                }
                else if (ivaMes1 > 0)
                {
                    iva1.TextColor = Color.Red;
                }
                else
                {
                    iva1.TextColor = Color.White;
                }
                mes1.Text = Utilities.GetMonthName(mesActualI);
                iva1.Text = Utilities.GetIva(ivaMes1);

                //Mes -2
                mesActualI = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                mesActualI = mesActualI.AddMonths(-2);
                mesActualF = mesActualI.AddMonths(1).AddDays(-1);

                temp = databse.GetIvaMes(mesActualI, mesActualF).Split('+');
                credito = Double.Parse(temp[0]);
                debito = Double.Parse(temp[1]);
                ivaMes2 = debito - credito;
                if (ivaMes2 < 0)
                {
                    iva2.TextColor = Color.Green;
                }
                else if (ivaMes2 > 0)
                {
                    iva2.TextColor = Color.Red;
                }
                else
                {
                    iva2.TextColor = Color.White;
                }
                mes2.Text = Utilities.GetMonthName(mesActualI);
                iva2.Text = Utilities.GetIva(ivaMes2);

                //Mes -3
                mesActualI = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                mesActualI = mesActualI.AddMonths(-3);
                mesActualF = mesActualI.AddMonths(1).AddDays(-1);
                
                temp = databse.GetIvaMes(mesActualI, mesActualF).Split('+');
                credito = Double.Parse(temp[0]);
                debito = Double.Parse(temp[1]);
                ivaMes3 = debito - credito;
                if (ivaMes3 < 0)
                {
                    iva3.TextColor = Color.Green;
                }
                else if (ivaMes3 > 0)
                {
                    iva3.TextColor = Color.Red;
                }
                else
                {
                    iva3.TextColor = Color.White;
                }
                mes3.Text = Utilities.GetMonthName(mesActualI);
                iva3.Text = Utilities.GetIva(ivaMes3);

                //Mes actual 
                mesActualI = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                mesActualF = mesActualI.AddMonths(1).AddDays(-1);

                temp = databse.GetIvaMes(mesActualI, mesActualF).Split('+');
                credito = Double.Parse(temp[0]);
                debito = Double.Parse(temp[1]);
                ivaTotal = debito - credito;
                if (ivaTotal < 0)
                {
                    iva.TextColor = Color.Green;
                }
                else if (ivaTotal > 0)
                {
                    iva.TextColor = Color.Red;
                }
                else
                {
                    iva.TextColor = Color.White;
                }
                mesActual.Text = Utilities.GetMonthName(mesActualI);
                iva.Text = Utilities.GetIva(ivaTotal);

                //Total
                Double ivaglbal = ivaMes1 + ivaMes2 + ivaMes3;
                if (ivaglbal > 0)
                {
                    ivaglbal = 0;
                }
                ivaglbal += ivaTotal;
                //total.Text = Utilities.GetTotal(ivaglbal);

            };
        }
    }
}
