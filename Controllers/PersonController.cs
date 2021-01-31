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
                    bool result = contDB.AddPersonRecord(prsn);
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
            return RedirectToAction("PersonList"); 
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
            
            PersonsVM pvm = new PersonsVM();
            pvm.Personss = contDB.GetPersonsByID(id);
            return View(pvm);
        }

        [HttpPost]
        public IActionResult DeletePerson(string prsn)
        {
            
           

            try
            {
                bool result = contDB.DeletePerson(Convert.ToInt32(prsn));
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

        [HttpGet]
        public IActionResult EditPerson(int id) 
        {
          
            Persons p = new Persons();
            p = contDB.GetPersonsByID(id);
            

            return View(p);
        }

        [HttpPost]
        public IActionResult EditPerson(Persons prsn)
        {           
            
            try
            {
                bool result = contDB.EditPerson(prsn);
                if (result)
                {
                    TempData["msg"] = "Data edited.";
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
