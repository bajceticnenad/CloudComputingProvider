using CloudComputingProvider.DataModel;
using CloudComputingProvider.DataModel.Order;
using CloudComputingProvider.DataModel.Software;

namespace CloudComputingProvider.Services.Interfaces.APIs
{
    public interface ICcpOrderService
    {
        Task<Response<List<SoftwareService>>> GetAvailableSoftwareServices(int customerId);
        Task<Response<Order>> CreateOrder(CreateOrderRequest createOrderRequest);
        Task<Response> CancelSubscription(CancelSubscriptionRequest cancelSubscriptionRequest);
        Task<Response> ExtendLicenceValidDate(ExtendLicenceValidDateRequest request);
        Task<Response> CancelSubscriptionLicence(CancelSubscriptionLicenceRequest request);
        Task<Response<List<SoftwareLicence>>> AddNewSubscriptionLicence(AddNewSubscriptionLicenceRequest request);
    }
}
