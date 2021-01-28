using AspNetCoreWebApp.Controllers;
using AspNetCoreWebApp.VMClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Model
{
    public class PersonController : Controller
    {
        
        ContactDBAccessLayer contDB = new ContactDBAccessLayer();

        private readonly ILogger<HomeController> _logger;
        private readonly IDbConnection _dbConnection;
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

       
        [HttpGet]
        public IActionResult CreatePerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePerson([Bind] Persons prsn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = contDB.AddPersonRecord(prsn);
                    TempData["msg"] = resp;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View();
        }

        public IActionResult PersonList()
        {

            PersonsVM pvm = new PersonsVM
            {
                Persons = contDB.GetPersons(),
            };
        

            return View(pvm);
        }

        public IActionResult DeletePerson(int id)
        {
            string connStrr = this.Configuration.GetConnectionString("Data Source=DESKTOP-8FK8A22; Initial Catalog = PhoneBook; Integrated Security = True");
            Persons person = new Persons();
            using (SqlConnection connection = new SqlConnection())
            {
                string sql = $"Delete From Persons Where Id='" + id + "'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        person.PersonID = Convert.ToInt32(dataReader["PersonID"]);
                        person.FirstName = Convert.ToString(dataReader["FirstName"]);
                        person.LastName = Convert.ToString(dataReader["LastName"]);
                        person.Email = Convert.ToString(dataReader["Email"]);
                        person.Telephone = Convert.ToString(dataReader["Telephone"]);
                        person.Location = Convert.ToString(dataReader["Location"]);
                        person.Company = Convert.ToString(dataReader["Company"]);
                    }
                }
                connection.Close();
            }

            return View(person);
        }
    }
}
