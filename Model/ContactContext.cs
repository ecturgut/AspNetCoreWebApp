using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApp.Model
{
    public class ContactContext: DbContext
    {
        public DbSet <Persons> Persons { get; set; }
        public DbSet <Report> Reports { get; set; }

    }
}
