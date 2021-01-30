using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Model
{
    public class AddReportDTO
    {
        [Required]
        public string Status { get; set; }

        [Required]
        public Nullable<DateTime> ReportDate { get; set; }

        public int PersonID { get; set; }
    }
}
