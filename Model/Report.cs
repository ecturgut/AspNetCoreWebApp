using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Model
{
    public class Report
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime ReportDate { get; set; }
        public int ReportStatusID { get; set; }

        public ReportStatus ReportStatus { get; set; }

    }
}
