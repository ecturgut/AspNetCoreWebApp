using AspNetCoreWebApp.Model;
using AspNetCoreWebApp.VMClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Controllers
{
    public class ReportController : Controller
    {
        ContactDBAccessLayer contDB = new ContactDBAccessLayer();

        [HttpGet]
        public IActionResult CreateReport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateReport([Bind] AddReportDTO rprt)
        {
            string resutMessage ="";
            try
            {
                if (ModelState.IsValid)
                {
                    var resp = contDB.AddReportRecord(rprt);
                    if(resp)
                    {
                        resutMessage = "Data save Successfully";
                    }
                    else
                    {
                        resutMessage = "data not save Successfully";
                    }

                    TempData["msg"] = resutMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View();
        }

        public IActionResult ReportList()
        {
            ReportVM rvm = new ReportVM
            {
                Report = contDB.GetReport(),
                
            };

            return View(rvm);
        }
    }
}
