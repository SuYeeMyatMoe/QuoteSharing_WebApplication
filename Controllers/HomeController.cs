using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuoteSharing_WebApplication.Configs;
using QuoteSharing_WebApplication.Models;
using QuoteSharing_WebApplication.Queries;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace QuoteSharing_WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                string query = QuoteQuery.GetQuoteListQuery;

                using SqlConnection connection = new(DbHelper.ConnectionString);
                await connection.OpenAsync();

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@IsDeleted", false);

                SqlDataAdapter adapter = new(command);
                DataTable dt = new();
                adapter.Fill(dt);

                await connection.CloseAsync();

                string jsonStr = JsonConvert.SerializeObject(dt);
                var quotes = JsonConvert.DeserializeObject<List<QuoteModel>>(jsonStr);

                return View(quotes);
            }
            catch (Exception ex)
            {
                return View(new List<QuoteModel>());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
