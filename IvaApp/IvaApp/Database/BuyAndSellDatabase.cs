using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IvaApp
{
    class BuyAndSellDatabase : IDisposable
    {
        private SQLiteConnection _connection;

        public BuyAndSellDatabase()
        {
            _connection = DependencyService.Get<ISQLite> ().GetConnection ();
            _connection.CreateTable<BuyAndSell>();
        }

        public bool InsertBuySell(BuyAndSell bs)
        {
            int value = _connection.Insert(bs);
            if (value == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public BuyAndSell GetBuySell(int id)
        {
            return _connection.Table<BuyAndSell>().FirstOrDefault(bs => bs.ID == id);
        }

        public List<BuyAndSell> GetBuySell()
        {
            //return _connection.Table<BuySell>().ToList();
            //return _connection.Table<BuySell>().OrderBy(bs => bs.Date).ToList();
            return _connection.Table<BuyAndSell>().Where(bs => bs.isBuy == true).OrderBy(bs => bs.Date).ToList();
                //OrderBy(bs => bs.Date).ToList();
        }

        public List<BuyAndSell> GetBuys(DateTime start, DateTime finish)
        {
            return _connection.Table<BuyAndSell>().Where(bs => bs.isBuy == true).Where(bs => bs.Date >= start && bs.Date <= finish).OrderBy(bs => bs.Date).ToList();
        }

        public List<BuyAndSell> GetSells(DateTime start, DateTime finish)
        {
            return _connection.Table<BuyAndSell>().Where(bs => bs.isBuy == false).Where(bs => bs.Date >= start && bs.Date <= finish).OrderBy(bs => bs.Date).ToList();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
