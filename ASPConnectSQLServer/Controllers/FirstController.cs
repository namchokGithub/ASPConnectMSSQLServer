using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections;

namespace ASPConnectSQLServer.Controllers
{
    public class FirstController : Controller
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        internal class User
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private readonly IConfiguration configuration;
        
        public FirstController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index()
        {
            string connnectionstring = configuration.GetConnectionString("DefaultConnectionString");

            SqlConnection connection = new SqlConnection(connnectionstring);

            connection.Open();

            SqlCommand com = new SqlCommand("Select count(*) from account", connection);
            var count = (int)com.ExecuteScalar();

            SqlCommand comUser = new SqlCommand("Select ID, Username, Password from account", connection);
            var user = comUser.ExecuteReader();

            List<Models.UserModel> userlist = new List<Models.UserModel>();

            while (user.Read()) { 
            
                userlist.Add(
                    new Models.UserModel() {
                        Id = (int)user["ID"],
                        Username = user["Username"].ToString(),
                        Password = user["Password"].ToString()
                    }
                );

            }

            ViewData["user"] = userlist;
            ViewData["TotalData"] = count;

            connection.Close();

            return View();
        }
    }
}
