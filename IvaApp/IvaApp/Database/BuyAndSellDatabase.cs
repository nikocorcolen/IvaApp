﻿using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IvaApp
{
    class BuySellDatabase : IDisposable
    {
        private SQLiteConnection _connection;

        public BuySellDatabase()
        {
            _connection = DependencyService.Get<ISQLite> ().GetConnection ();
            _connection.CreateTable<BuySell>();
        }

        public bool InsertBuySell(BuySell bs)
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

        public BuySell GetBuySell(int id)
        {
            return _connection.Table<BuySell>().FirstOrDefault(bs => bs.ID == id);
        }

        public List<BuySell> GetBuySell()
        {
            //return _connection.Table<BuySell>().ToList();
            //return _connection.Table<BuySell>().OrderBy(bs => bs.Date).ToList();
            return _connection.Table<BuySell>().Where(bs => bs.isBuy == true).OrderBy(bs => bs.Date).ToList();
                //OrderBy(bs => bs.Date).ToList();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
