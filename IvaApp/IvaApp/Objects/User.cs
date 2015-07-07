using System;
using SQLite.Net.Attributes;

namespace IvaApp
{
    public class User
    {

        [PrimaryKey]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }

        public User()
        {
        }
    }
}
