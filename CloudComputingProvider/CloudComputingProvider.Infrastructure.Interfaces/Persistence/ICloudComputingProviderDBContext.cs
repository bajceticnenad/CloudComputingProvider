using CloudComputingProvider.DataModel.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CloudComputingProvider.Infrastructure.Interfaces.Persistence
{
    public interface ICloudComputingProviderDBContext
    {
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Customers> Customers { get; set; }
        DbSet<CustomerAccounts> CustomerAccounts { get; set; }
        DbSet<Subscriptions> Subscriptions { get; set; }
        DbSet<SubscriptionDetails> SubscriptionDetails { get; set; }
        DbSet<States> States { get; set; }
    }
}
