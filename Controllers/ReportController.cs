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
            string resutMessage = "";
            try
            {
                if (ModelState.IsValid)
                {
                    bool resp = contDB.AddReportRecord(rprt);
                    if (resp)
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
            return RedirectToAction("ReportList");
        }

        public IActionResult ReportList()
        {
            ReportVM rvm = new ReportVM
            {
                Report = contDB.GetReport(),

            };

            return View(rvm);
        }

        [HttpGet]
        public IActionResult DeleteReport(int id)
        {
            
            ReportVM rvm = new ReportVM();
            rvm.Reportt = contDB.GetReportByID(id);
            return View(rvm);
        }

        [HttpPost]
        public IActionResult DeleteReport(string rvm)
        {
            
            
            try
            {
                bool result = contDB.DeleteReport(Convert.ToInt32(rvm));

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
            return RedirectToAction("ReportList");
        }

        [HttpGet]
        public IActionResult EditReport(int id)
        {

            Report r = new Report();
            r = contDB.GetReportByID(id);

            return View(r);
        }

        [HttpPost]
        public IActionResult EditReport(Report rprt)
        {
            
            try
            {
                bool result = contDB.EditReport(rprt);
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

            return RedirectToAction("ReportList");
        }

    }
}
