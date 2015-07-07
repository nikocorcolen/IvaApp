using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvaApp
{
    public class BuyAndSell
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public Boolean isBuy { get; set; }
        public string ProductName { get; set; }
        public int Factura { get; set; }

        public BuyAndSell()
        {
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", ProductName, Price, ID, Date, Factura);
        }
    }
}
