using SQLite;

namespace Finora.Models
{
    public class Saving
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Month { get; set; }
        public int UserId { get; set; }
        public decimal SavedAmount { get; set; }
        public decimal GoalAmount {  get; set; }
    }
}