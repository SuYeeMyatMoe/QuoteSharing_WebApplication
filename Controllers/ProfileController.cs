using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using QuoteSharing_WebApplication.Models;
using QuoteSharing_WebApplication.Configs;

namespace QuoteSharing_WebApplication.Controllers
{
    public class ProfileController : Controller
    {

        public IActionResult Settings()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Account");
            }

            UserModel user = new UserModel();
            using (SqlConnection con = new SqlConnection(DbHelper.ConnectionString))
            {
                string query = "SELECT UserID, UserName, Email FROM Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user.UserID = reader.GetInt32(0);
                        user.UserName = reader.GetString(1);
                        user.Email = reader.GetString(2);
                    }
                }
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateProfile(UserModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(DbHelper.ConnectionString))
                {
                    string query = "UPDATE Users SET UserName = @UserName, Email = @Email WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserName", model.UserName);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@UserID", model.UserID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                TempData["Success"] = "Profile updated successfully!";
            }

            return RedirectToAction("Settings");
        }
    }
}
