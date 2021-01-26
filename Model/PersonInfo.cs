using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Model
{
    public class PersonInfo
    {
        public int ID { get; set; }
        public int Telephone { get; set; }
        public string Email { get; set; }
        public int LocationID { get; set; }
        public int ReportID { get; set; }
        public int PersonID { get; set; }

        public Person Person { get; set; }
        public Report Report { get; set; }
        public Location Location { get; set; }

    }
}
