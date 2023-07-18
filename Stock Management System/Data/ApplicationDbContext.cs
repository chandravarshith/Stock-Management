using Microsoft.EntityFrameworkCore;
using Stock_Management_System.Models;

namespace Stock_Management_System.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }

}
