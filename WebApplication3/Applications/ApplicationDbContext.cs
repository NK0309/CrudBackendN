using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Applications
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public DbSet<Customers> Customer { get; set; }


    }
}
