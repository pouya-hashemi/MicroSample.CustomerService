using MicroSample.CustomerService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroSample.CustomerService.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Customer> Customers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
