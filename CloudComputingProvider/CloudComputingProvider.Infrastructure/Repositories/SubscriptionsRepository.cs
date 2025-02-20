using CloudComputingProvider.DataModel.Domain.Models;
using CloudComputingProvider.Infrastructure.Interfaces.Persistence;
using CloudComputingProvider.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudComputingProvider.Infrastructure.Repositories
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        #region PrivateFields
        private readonly ILogger<SubscriptionsRepository> _logger;
        private readonly ICloudComputingProviderDBContext _cloudComputingProviderDBContext;

        #endregion PrivateFields

        #region Constructor
        public SubscriptionsRepository(ILogger<SubscriptionsRepository> logger, ICloudComputingProviderDBContext cloudComputingProviderDBContext)
        {
            _logger = logger;
            _cloudComputingProviderDBContext = cloudComputingProviderDBContext;
        }
        #endregion Constructor

        #region PublicMethods
        public async Task<List<Subscriptions>> GetSubscriptions(int customerAccountId, CancellationToken cancellationToken)
        {
            return await _cloudComputingProviderDBContext.Subscriptions
                .AsNoTracking()
                .Include(s => s.SubscriptionDetails.Where(sd => !sd.IsDeleted))
                .Include(c => c.CustomerAccount)
                .Include(st => st.State)
                .Where(x => !x.IsDeleted && x.CustomerAccountId == customerAccountId)
                .ToListAsync(cancellationToken);
        }

        public async Task<SubscriptionDetails> GetLicenceById(int licenceId)
        {
            return await _cloudComputingProviderDBContext.SubscriptionDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LicenceId == licenceId);
        }

        public async Task<int> ModifySubscriptionDetails(SubscriptionDetails newSubscriptionDetails, CancellationToken cancellationToken = default)
        {
            var subscriptionDetails = await _cloudComputingProviderDBContext.SubscriptionDetails
                .FirstAsync(x => x.Id == newSubscriptionDetails.Id);

            subscriptionDetails.ValidToDate = newSubscriptionDetails.ValidToDate;
            subscriptionDetails.LicenceId = newSubscriptionDetails.LicenceId;
            subscriptionDetails.Licence = newSubscriptionDetails.Licence;
            subscriptionDetails.IsDeleted = newSubscriptionDetails.IsDeleted;
            subscriptionDetails.ModifiedDate = DateTime.Now;
            subscriptionDetails.ModifiedBy = DataModel.Enums.MockUserLog.ApiUser.ToString();

            var result = await _cloudComputingProviderDBContext.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<Subscriptions> GetSubscriptionById(int subscriptionId)
        {
            return await _cloudComputingProviderDBContext.Subscriptions
                .AsNoTracking()
                .Include(s => s.SubscriptionDetails.Where(sd => !sd.IsDeleted))
                .Include(c => c.CustomerAccount)
                .Include(st => st.State)
                .FirstOrDefaultAsync(x => x.Id == subscriptionId);
        }

        public async Task<int> ModifySubscription(Subscriptions newSubscription, CancellationToken cancellationToken)
        {
            var subscription = await _cloudComputingProviderDBContext.Subscriptions
                .FirstAsync(x => x.Id == newSubscription.Id);

            subscription.SoftwareId = newSubscription.SoftwareId;
            subscription.SoftwareName = newSubscription.SoftwareName;
            subscription.CustomerAccountId = newSubscription.CustomerAccountId;
            subscription.StateId = newSubscription.StateId;
            subscription.Quantity = newSubscription.Quantity;
            subscription.IsDeleted = newSubscription.IsDeleted;
            subscription.ModifiedDate = DateTime.Now;
            subscription.ModifiedBy = DataModel.Enums.MockUserLog.ApiUser.ToString();

            var result = await _cloudComputingProviderDBContext.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<int> RemoveSubscriptionLicences(int subscriptionId, int quantuty, 
            IEnumerable<SubscriptionDetails> subscriptionDetails, CancellationToken cancellationToken)
        {
            var subscription = await _cloudComputingProviderDBContext.Subscriptions
                .Include(s => s.SubscriptionDetails)               
                .FirstAsync(x => x.Id == subscriptionId);

            subscription.Quantity = quantuty;
            subscription?.SubscriptionDetails
                .Where(x => subscriptionDetails.Any(a => a.Id == x.Id)).ToList()
                .ForEach(x =>
                {
                    x.IsDeleted = true;
                    x.ModifiedDate = DateTime.Now;
                    x.ModifiedBy = DataModel.Enums.MockUserLog.ApiUser.ToString();
                });

            return await _cloudComputingProviderDBContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> AddSubscriptionList(List<Subscriptions> subscriptions, CancellationToken cancellationToken)
        {
            await _cloudComputingProviderDBContext.Subscriptions
            .AddRangeAsync(subscriptions, cancellationToken);

            var result = await _cloudComputingProviderDBContext.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<int> AddNewSubscriptionLicences(int subscriptionId, int quantuty,
            IEnumerable<SubscriptionDetails> subscriptionDetails, CancellationToken cancellationToken)
        {
            var subscription = await _cloudComputingProviderDBContext.Subscriptions
                .Include(s => s.SubscriptionDetails)
                .FirstAsync(x => x.Id == subscriptionId);

            subscription.Quantity = quantuty;
            foreach (var item in subscriptionDetails)
            {
                subscription.SubscriptionDetails.Add(item);
            }

            return await _cloudComputingProviderDBContext.SaveChangesAsync(cancellationToken);
        }
        #endregion PublicMethods
    }
}
