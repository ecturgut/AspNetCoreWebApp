using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Model
{
    public class Report : Persons
    {
        public int ID { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime ReportDate { get; set; }

        public Persons Person { get; set; }

    }
}
