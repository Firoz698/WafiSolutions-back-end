using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafiSolutions.core;

namespace WafiSolutions.infracturcture
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext>Options ) : base(Options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
