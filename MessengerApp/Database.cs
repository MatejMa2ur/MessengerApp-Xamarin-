using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using MessengerApp.Models;

namespace MessengerApp
{
    public class Database
    {
        private SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetPeopleAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<int> SavePersonAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public bool RemoveAll()
        {
            _database.DeleteAllAsync<User>().Wait();
            return true;
        }
    }
}