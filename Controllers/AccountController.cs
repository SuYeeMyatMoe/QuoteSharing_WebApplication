using Microsoft.AspNetCore.Mvc;
using QuoteSharing_WebApplication.Models;
using QuoteSharing_WebApplication.Queries;

namespace QuoteSharing_WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository _accountRepo = new AccountRepository();

        // GET: Login Page
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login Authentication
        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            var user = _accountRepo.AuthenticateUser(model.Email, model.Password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserID", user.UserID);
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("QuoteListPage", "Quote");
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }

        // GET: Signup Page
        public IActionResult Signup()
        {
            return View();
        }

        // POST: Signup 
        [HttpPost]
        public IActionResult Signup(UserModel model)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = _accountRepo.RegisterUser(model);
                if (isRegistered)
                {
                    return RedirectToAction("Login");
                }
                ViewBag.Error = "Signup failed. Try again.";
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}