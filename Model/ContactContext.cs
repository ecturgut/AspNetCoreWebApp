using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Model
{
    public class ContactContext: DbContext
    {
        public DbSet <Person> Persons { get; set; }
        public DbSet<PersonInfo> PersonInfos { get; set; }
        public DbSet <Location> Locations { get; set; }
        public DbSet <Report> Reports { get; set; }
        public DbSet <ReportStatus> ReportStats { get; set; }

    }
}
