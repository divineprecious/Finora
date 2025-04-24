using Finora.Models;
using SQLite;

namespace Finora.Services
{
    public static class DatabaseHelper
    {
        private static SQLiteAsyncConnection database;

        public static async Task Init()
        {
            if (database is not null)
            {
                return;
            }
            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await database.CreateTableAsync<User>();
            await database.CreateTableAsync<Transaction>();
        }

        //Users
        public static async Task<int> AddUser(User user)
        {
            await Init();
            return await database.InsertAsync(user);
        }

        public static async Task<int> DeleteUser(User user)
        {
            await Init();
            return await database.DeleteAsync(user);
        }

        public static async Task<User> Authenticate(string email, string password) 
        {
            await Init();
            return await database.Table<User>().Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        }

        //Transactions
        public static async Task<int> AddTransaction(Transaction transaction)
        {
            await Init();
            return await database.InsertAsync(transaction);
        }

        public static async Task<int> DeleteTransaction(Transaction transaction)
        {
            await Init();
            return await database.DeleteAsync(transaction);
        }

        public static async Task<List<Transaction>> GetTransactionsForUser(int userId, string type = null, string? category = null, int? month = null)
        {
            await Init();

            var query = database.Table<Transaction>().Where(t => t.UserId == userId);

            //Filter by transaction type if provided
            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(t => t.Type == type);   
            }

            //Filter by category if given
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(t => t.Category == category);
            }

            var results = await query.ToListAsync();

            if (month != null)
            {
                results = results.Where(t => t.Date.Month == month.Value).ToList();
            }

            return results;
        }
    }
}
