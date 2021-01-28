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
        public IActionResult CreateReport([Bind] Report rprt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = contDB.AddReportRecord(rprt);
                    TempData["msg"] = resp;
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
            //ReportVM rvm = new ReportVM
            //{
            //    Report = contDB.GetReport(),
            //};

            return View();
        }
    }
}
