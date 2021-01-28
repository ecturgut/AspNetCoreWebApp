using AspNetCoreWebApp.VMClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Model
{
    public class PersonController : Controller
    {
        
        ContactDBAccessLayer contDB = new ContactDBAccessLayer();

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
    }
}
