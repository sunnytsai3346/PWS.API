using Microsoft.EntityFrameworkCore;
using PWS.API.Models;

namespace PWS.API.Data
{
    public class PMSDbContext : DbContext
    {
        //constructer
        public PMSDbContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}
