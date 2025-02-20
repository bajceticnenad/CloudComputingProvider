using CloudComputingProvider.DataModel.Software;

namespace CloudComputingProvider.DataModel.Order
{
    public class CreateOrderRequest
    {
        public int CustomerAccountId { get; set; }
        public List<SoftwareService> SoftwareServices { get; set; }
    }
}
