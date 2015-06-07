using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvaApp
{
    public static class Utilities
    {
        public static string GetMonthName()
        {
            CultureInfo ci = new CultureInfo("es-ES");
            string month = DateTime.Now.ToString("MMMM", ci);
            return month;
        }

        public static DateTime GetStartMonth()
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return firstDayOfMonth;
        }

        public static DateTime GetFinishMonth()
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return lastDayOfMonth;
        }
    }
}
