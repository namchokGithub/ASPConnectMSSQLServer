using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ASPConnectSQLServer.Controllers
{
    public class FirstController : Controller
    {
        private readonly IConfiguration configuration;

        public FirstController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index()
        {
            string connnectionstring = configuration.GetConnectionString("DefaultConnectionString");

            SqlConnection connection = new SqlConnection("connnectionstring");

            connection.Open();

            SqlCommand com = new SqlCommand("Select count(*) from account", connection);
            var count = (int)com.ExecuteScalar();

            ViewData["TotalData"] = count;

            connection.Close();

            return View();
        }
    }
}
