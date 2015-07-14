using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using IvaApp.Droid;
using System.IO;

[assembly: Dependency(typeof(Save))]
namespace IvaApp.Droid
{
    class Save : ISave
    {
        private CultureInfo ci = new CultureInfo("es-CL");
        public void SaveText(List<BuyAndSell> list)
        {
            string filename = "temp.csv";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);

            StringBuilder builder = new StringBuilder();

            //Split(" ")
            //0 = nombre; 1 = total; 3 = fecha; 6 = factura

            builder.AppendLine(string.Join(";", "Nombre", "Monto Total", "Fecha", "Numero de Factura"));
            for (int i = 0; i < list.Count; i++) // Loop through List with for
            {
                String[] temp = list[i].ToString().Split(' ');
                String nombre = "";
                //es 7 por la cantidad de datos que se guardan en un registro
                //si son mas de 7 significa que el nombre tiene mas de una palabra
                for (int j = 0; j <= (temp.Length - 7); j++)
                {
                    nombre += temp[j] + " ";
                }
                string monto =  Convert.ToDouble(temp[temp.Length - 6]).ToString("C", ci);
                string factura = Convert.ToDouble(temp[temp.Length - 1]).ToString("C", ci);
                string fecha = Convert.ToDateTime(temp[temp.Length - 4]).ToString("dd/MM/yyyy");
                builder.AppendLine(string.Join(";", nombre, monto, fecha , factura));
            }

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                File.WriteAllText(filePath, builder.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Ouch!" + e.ToString());
            }
            //using (var streamWriter = new StreamWriter(filename, true))
            //{
            //    streamWriter.WriteLine(DateTime.UtcNow);
            //}
        }
    }
}