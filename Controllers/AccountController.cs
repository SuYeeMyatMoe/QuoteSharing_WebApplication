using Microsoft.AspNetCore.Mvc;
using QuoteSharing_WebApplication.Configs;
using QuoteSharing_WebApplication.Models;
using QuoteSharing_WebApplication.Queries;
using System.Data.SqlClient;

namespace QuoteSharing_WebApplication.Controllers
{
    public class AccountController : Controller
    {

        // GET: Login Page
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle Login
        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            using (SqlConnection con = new SqlConnection(DbHelper.ConnectionString))
            {
                string query = "SELECT UserID, UserName, Email FROM Users WHERE Email = @Email AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        HttpContext.Session.SetString("UserID", reader["UserID"].ToString());
                        HttpContext.Session.SetString("UserName", reader["UserName"].ToString());
                        HttpContext.Session.SetString("UserEmail", reader["Email"].ToString());

                        return RedirectToAction("QuoteListPage", "Quote");
                    }
                }
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }

        // GET: Signup Page
        public IActionResult Signup()
        {
            return View();
        }

        // POST: Handle Signup
        [HttpPost]
        public IActionResult Signup(UserModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(DbHelper.ConnectionString))
                {
                    string query = "INSERT INTO Users (UserName, Email, Password) VALUES (@UserName, @Email, @Password)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserName", model.UserName);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Password", model.Password);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                TempData["Success"] = "Account created successfully! Please log in.";
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // Logout and clear session
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}