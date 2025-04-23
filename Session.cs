using Finora.Models;
namespace Finora
{
    public static class Session
    {   
        //Stores logged in user
        public static User? CurrentUser { get; set; }
    }
}
