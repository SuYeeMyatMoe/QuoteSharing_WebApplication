using QuoteSharing_WebApplication.Configs;
using QuoteSharing_WebApplication.Models;
using System.Data.SqlClient;

namespace QuoteSharing_WebApplication.Queries
{
    public class AccountRepository
    {// Login Authentication
        public UserModel AuthenticateUser(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = "SELECT UserID, UserName, Email FROM Users WHERE Email = @Email AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password); // Plain text for now (should be hashed)

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel
                            {
                                UserID = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                Email = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return null; // Return null if user not found
        }

        // Register New User
        public bool RegisterUser(UserModel user)
        {
            using (SqlConnection conn = new SqlConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (UserName, Email, Password) VALUES (@UserName, @Email, @Password)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password); // Should hash before storing

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
