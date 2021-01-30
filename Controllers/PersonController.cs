using AspNetCoreWebApp.Controllers;
using AspNetCoreWebApp.VMClasses;
using Microsoft.AspNetCore.Http;
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
                    var result = contDB.AddPersonRecord(prsn);
                    if (result)
                    {
                        TempData["msg"] = "Data saved.";
                    }
                    else
                    {
                        TempData["msg"] = "Something wrong. Please try again later.";
                    }

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

        public IActionResult Connect() 
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeletePerson(int id)
        {
            ContactDBAccessLayer dba = new ContactDBAccessLayer();
            PersonsVM pvm = new PersonsVM();
            pvm.Personss = dba.GetPersonsByID(id);
            return View(pvm);
        }

        [HttpPost]
        public IActionResult DeletePerson(string prsn)
        {
            ContactDBAccessLayer dba = new ContactDBAccessLayer();
            var result = dba.DeletePerson(Convert.ToInt32(prsn));

            try
            {
                if (result)
                {
                    TempData["msg"] = "Data deleted.";
                }
                else
                {
                    TempData["msg"] = "Something wrong. Please try again later.";
                }


            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("PersonList");
        }
    }
}
