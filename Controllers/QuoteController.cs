using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuoteSharing_WebApplication.Configs;
using QuoteSharing_WebApplication.Models;
using QuoteSharing_WebApplication.Queries;
using System.Data;
using System.Data.SqlClient;

namespace QuoteSharing_WebApplication.Controllers
{
    public class QuoteController : Controller
    {


        [ActionName("QuoteListPage")]

        // GET: QuoteController
        public async Task<ActionResult> Index()
        {
            try
            {
                string query = QuoteQuery.GetQuoteListQuery;

                SqlConnection connection = new(DbHelper.ConnectionString);
                await connection.OpenAsync();

                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@IsDeleted", false);

                SqlDataAdapter adapter = new(command);
                DataTable dt = new();
                adapter.Fill(dt);

                await connection.CloseAsync();

                string jsonStr = JsonConvert.SerializeObject(dt);//using Newtonsoft.Json;
                var lst = JsonConvert.DeserializeObject<List<QuoteModel>>(jsonStr);

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

            // GET: QuoteController/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuoteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuoteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuoteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuoteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuoteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuoteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index)); ;
            }
            catch
            {
                return View();
            }
        }
    }
}
