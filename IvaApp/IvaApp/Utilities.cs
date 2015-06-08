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
        private static CultureInfo ci = new CultureInfo("es-CL");

        public static string GetMonthName(DateTime date)
        {
            string month = date.ToString("MMMM", ci);
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

        public static string GetNeto(Double total)
        {
            return Math.Round((100 * total) / 119,0).ToString("C",ci);
        }

        public static string GetIva(Double total)
        {
            return Math.Round((total * 19) / 119, 0).ToString("C", ci);
        }

        public static string GetTotal(Double total)
        {
            return Math.Round( total, 0).ToString("C", ci);
        }
    }
}
