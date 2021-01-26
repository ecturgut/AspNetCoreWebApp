using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Model
{
    public class PersonController : Controller
    {
        public IActionResult CreatePerson()
        {
            return View();
        }
        public IActionResult PersonList()
        {
            return View();
        }
    }
}
