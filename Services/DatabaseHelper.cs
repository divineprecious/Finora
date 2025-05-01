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
            await database.CreateTableAsync<Saving>();
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

        //Savings
        public static async Task<int> AddSavings(Saving savings)
        {
            await Init();
            return await database.InsertAsync(savings);
        }

        public static async Task<int> UpdateSavedAmountAsync(int userId, int month, decimal newAmount)
        {
            var savings = await database.Table<Saving>()
                                         .Where(s => s.UserId == userId && s.Month == month)
                                         .FirstOrDefaultAsync();

            if (savings != null)
            {
                savings.SavedAmount = newAmount;
                return await database.UpdateAsync(savings);
            }
            return 0;
        }

        public static async Task<int> UpdateGoalAmountAsync(int userId, int month, decimal newAmount)
        {
            var savings = await database.Table<Saving>()
                                         .Where(s => s.UserId == userId && s.Month == month)
                                         .FirstOrDefaultAsync();

            if (savings != null)
            {
                savings.GoalAmount = newAmount;
                return await database.UpdateAsync(savings);
            }
            return 0;
        }

        public static async Task <Saving> GetSavingsForUser(int userId, int month)
        {
            await Init();

            return await database.Table<Saving>().Where(s => s.UserId == userId && s.Month == month).FirstOrDefaultAsync();
        }

    }
}
