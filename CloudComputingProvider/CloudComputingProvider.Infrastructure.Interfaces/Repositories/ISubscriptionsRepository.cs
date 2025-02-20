using CloudComputingProvider.DataModel;
using CloudComputingProvider.DataModel.Domain.Models;
using System.Threading;

namespace CloudComputingProvider.Infrastructure.Interfaces.Repositories
{
    public interface ISubscriptionsRepository
    {
        Task<List<Subscriptions>> GetSubscriptions(int customerAccountId, CancellationToken cancellationToken);
        Task<SubscriptionDetails> GetLicenceById(int licenceId);
        Task<int> ModifySubscriptionDetails(SubscriptionDetails newSubscriptionDetails, CancellationToken cancellationToken);
        Task<Subscriptions> GetSubscriptionById(int subscriptionId);
        Task<int> ModifySubscription(Subscriptions newSubscription, CancellationToken cancellationToken);
        Task<int> RemoveSubscriptionLicences(int subscriptionId, int quantuty, IEnumerable<SubscriptionDetails> subscriptionDetails, 
            CancellationToken cancellationToken);
        Task<int> AddSubscriptionList(List<Subscriptions> subscriptions, CancellationToken cancellationToken);

        Task<int> AddNewSubscriptionLicences(int subscriptionId, int quantuty, IEnumerable<SubscriptionDetails> subscriptionDetails,
            CancellationToken cancellationToken);
    }
}
