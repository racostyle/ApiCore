using Microsoft.AspNetCore.Mvc;

namespace ApiHost.Controllers
{
    public class LogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public LogController()
        {
            
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] LogDTO product)
        //{
        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        string query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price);";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@Name", product.Name);
        //            command.Parameters.AddWithValue("@Price", product.Price);

        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }

        //    return Ok("Product created successfully.");
        //}
    }
}
