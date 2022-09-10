namespace Inventory_Tracker.Models
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string OldUsername { get; set; }
    }
}
