using AspNetCoreWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.VMClasses
{
    public class ReportVM
    {
        public Report Reportt { get; set; }
        public List<Report> Report { get; set; }
    }
}
