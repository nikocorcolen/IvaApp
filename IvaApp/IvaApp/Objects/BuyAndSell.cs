using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvaApp
{
    class BuySell
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public Boolean isBuy { get; set; }
        public string Name { get; set; }
        public int Factura { get; set; }
        public string Description { get; set; }

        public BuySell()
        {
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", ID, Amount, Date, Name, Factura, Description);
        }
    }
}
