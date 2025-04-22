using SQLite;

namespace Finora.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; } 
        public string Type { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}
