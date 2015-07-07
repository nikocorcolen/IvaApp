using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public void SaveText(List<BuyAndSell> list)
        {
            string filename = "temp.csv";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);

            StringBuilder builder = new StringBuilder();

            //Split(" ")
            //0 = nombre; 1 = total; 3 = fecha; 6 = factura
            //builder.AppendLine(string.Join(";", list[0]));
            //builder.AppendLine(string.Join(";", "col1", "col2"));
            //System.IO.File.WriteAllText(filePath, text);
            System.IO.File.WriteAllText(filePath, builder.ToString());

            for (int i = 0; i < list.Count; i++) // Loop through List with for
            {
                String[] temp = list[i].ToString().Split(' ');
                builder.AppendLine(string.Join(";", temp[0], temp[1], temp[3], temp[6]));
            }
            //using (var streamWriter = new StreamWriter(filename, true))
            //{
            //    streamWriter.WriteLine(DateTime.UtcNow);
            //}
        }
    }
}