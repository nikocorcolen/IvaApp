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

        public BuySell()
        {
        }
    }
}
