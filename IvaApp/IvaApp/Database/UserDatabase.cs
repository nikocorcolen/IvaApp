using System;
using SQLite.Net;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IvaApp
{
    public class UserDatabase : IDisposable
    {
        private SQLiteConnection _connection;

        public UserDatabase()
        {
            _connection = DependencyService.Get<ISQLite> ().GetConnection ();
            _connection.CreateTable<User> ();
        }

        public bool InsertUser(User user)
        {
            int value = _connection.Insert(user);
            if (value == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public User GetMail(String username)
        {
            return _connection.Table<User>().FirstOrDefault(u => u.Username == username);
        }

        public void UpdateUsuario(User u)
        {
            _connection.Update(u);
        }

        public void DeleteUsuario(User u)
        {
            _connection.Delete(u);
        }

        public User GetUsuario(string username)
        {
            return _connection.Table<User>().FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetUsuarios()
        {
            //return connection.Table<Usuario>().ToList();
            return _connection.Table<User>().OrderBy(u => u.Username).ToList();
        }
 
        public IEnumerable<User> GetThoughts() {
            return (from t in _connection.Table<User>()
                    select t).ToList ();
        }

        public void DeleteThought(int id) {
            _connection.Delete<User>(id);
        }
 
        //public void AddThought(string thought) {
        //    var newThought = new User
        //    {
        //        Thought = thought,
        //        CreatedOn = DateTime.Now
        //    };
 
        //    _connection.Insert (newThought);
        //}

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
