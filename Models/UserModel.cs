namespace QuoteSharing_WebApplication.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // In production, passwords should be hashed
    }
}

