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

                return View(lst);
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
        [ActionName("CreateQuotePage")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuoteController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ActionName("SaveQuoteAsync")]
        public async Task<ActionResult> SaveQuoteAsync(QuoteRequestModel requestModel)
        {
            try
            {
                string query = QuoteQuery.CreateBlogQuery;
                var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@QuoteWriter", requestModel.QuoteWriter),
                new SqlParameter("@QuoteText", requestModel.QuoteText),
                new SqlParameter("@UploadedEmail ", requestModel.UploadedEmail),
            };

                SqlConnection connection = new(DbHelper.ConnectionString);
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters.ToArray());

                int result = await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();

                if (result > 0) // row effected > 0
                {
                    TempData["success"] = "Saving Successful.";
                }
                else
                {
                    TempData["fail"] = "Saving Fail.";
                }

                return RedirectToAction("QuoteListPage");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: QuoteController/Edit/5
        [ActionName("EditQuotePage")]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                string query = QuoteQuery.GetQuoteByIdQuery;

                SqlConnection connection = new SqlConnection(DbHelper.ConnectionString);
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@QuoteID", id);
                command.Parameters.AddWithValue("@IsDeleted", false);

                SqlDataAdapter adapter = new(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                string jsonStr = JsonConvert.SerializeObject(dt);
                var lst = JsonConvert.DeserializeObject<List<QuoteModel>>(jsonStr);

                await connection.CloseAsync();

                return View(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST: QuoteController/Edit/5

        [ActionName("UpdateQuoteAsync")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuoteAsync(QuoteRequestModel requestModel, int id)
        {
            try
            {
                string query = QuoteQuery.UpdateQuoteQuery;

                var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@QuoteWriter", requestModel.QuoteWriter),
                new SqlParameter("@QuoteText", requestModel.QuoteText),
                new SqlParameter("@UploadedEmail", requestModel.UploadedEmail),
                new SqlParameter("@QuoteID", id),
                new SqlParameter("@IsDeleted", false),
            };

                SqlConnection connection = new SqlConnection(DbHelper.ConnectionString);
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters.ToArray());

                int result = await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();

                if (result > 0)
                {
                    TempData["success"] = "Updating Successful.";
                }
                else
                {
                    TempData["fail"] = "Updating Fail.";
                }

                return RedirectToAction("QuoteListPage");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [ActionName("DeleteQuoteAsync")]
        public async Task<IActionResult> DeleteQuoteAsync(int id)
        {
            try
            {
                string query = QuoteQuery.DeleteBlogQuery;
                var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@QuoteID", id),
                new SqlParameter("@IsDeleted", true),
            };

                SqlConnection connection = new SqlConnection(DbHelper.ConnectionString);
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters.ToArray());

                int result = await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();

                if (result > 0)
                {
                    TempData["success"] = "Deleting Successful.";
                }
                else
                {
                    TempData["fail"] = "Deleting Fail.";
                }

                return RedirectToAction("BlogListPage");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
