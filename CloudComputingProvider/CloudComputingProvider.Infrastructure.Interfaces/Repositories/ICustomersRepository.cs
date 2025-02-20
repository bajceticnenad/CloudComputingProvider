using CloudComputingProvider.DataModel;
using CloudComputingProvider.DataModel.Domain.Models;

namespace CloudComputingProvider.Infrastructure.Interfaces.Repositories
{
    public interface ICustomersRepository
    {
        Task<List<CustomerAccounts>> GetCustomerAccounts(int customerId, CancellationToken cancellationToken);
    }
}
