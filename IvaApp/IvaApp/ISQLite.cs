using System;
using SQLite.Net;

namespace IvaApp
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}