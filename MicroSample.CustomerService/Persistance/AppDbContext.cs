using MicroSample.CustomerService.Entities;
using MicroSample.CustomerService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MicroSample.CustomerService.Persistance
{
    public class AppDbContext:DbContext,IAppDbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
