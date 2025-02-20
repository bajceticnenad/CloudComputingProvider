using CloudComputingProvider.DataModel;
using CloudComputingProvider.DataModel.Order;
using CloudComputingProvider.DataModel.Software;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudComputingProvider.Services.Interfaces.Mock
{
    public interface IMockCcpApi
    {
        [Get("/available-software-services")]
        Task<List<SoftwareService>> GetAvailableSoftwareServices([AliasAs("customerId")] int customerId);

        [Post("/create-order")]
        Task<Order> CreateOrder([Body] CreateOrderRequest createOrderRequest);

        [Post("/cancel-subscription")]
        Task<Response> CancelSubscription([Body] CancelSubscriptionRequest cancelSubscriptionRequest);

        [Post("/extend-licence-valid-date")]
        Task<Response> ExtendLicenceValidDate([Body] ExtendLicenceValidDateRequest request);

        [Post("/cancel-subscription-licence")]
        Task<Response> CancelSubscriptionLicence([Body] CancelSubscriptionLicenceRequest request);

        [Post("/add-new-subscription-licence")]
        Task<List<SoftwareLicence>> AddNewSubscriptionLicence([Body] AddNewSubscriptionLicenceRequest request);
    }
}