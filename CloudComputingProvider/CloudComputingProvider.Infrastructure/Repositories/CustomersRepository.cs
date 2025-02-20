using CloudComputingProvider.DataModel.Domain.Models;
using CloudComputingProvider.Infrastructure.Interfaces.Persistence;
using CloudComputingProvider.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudComputingProvider.Infrastructure.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        #region PrivateFields
        private readonly ILogger<CustomersRepository> _logger;
        private readonly ICloudComputingProviderDBContext _cloudComputingProviderDBContext;

        #endregion PrivateFields

        #region Constructor
        public CustomersRepository(ILogger<CustomersRepository> logger, ICloudComputingProviderDBContext cloudComputingProviderDBContext)
        {
            _logger = logger;
            _cloudComputingProviderDBContext = cloudComputingProviderDBContext;
        }
        #endregion Constructor

        #region PublicMethods
        public async Task<List<CustomerAccounts>> GetCustomerAccounts(int customerId, CancellationToken cancellationToken)
        {
            return await _cloudComputingProviderDBContext.CustomerAccounts
                .AsNoTracking()
                .Where(x => !x.IsDeleted && x.CustomerId == customerId)
                .ToListAsync(cancellationToken);
        }
        #endregion PublicMethods
    }
}
