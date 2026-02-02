using Microsoft.Azure.Cosmos.Core.Collections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract12_TRPO
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder
        optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-SHKSOAD\\SQLEXPRESS;Database=Pract12;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
        }
    }
}
