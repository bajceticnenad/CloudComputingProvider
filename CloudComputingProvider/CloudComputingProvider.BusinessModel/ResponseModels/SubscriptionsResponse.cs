namespace CloudComputingProvider.BusinessModel.ResponseModels
{
    public class SubscriptionsResponse
    {
        public int Id { get; set; }
        public int SoftwareId { get; set; }
        public string SoftwareName { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public List<SubscriptionDetailsResponse> SubscriptionDetails { get; set; }
        public CustomerAccountsResponse CustomerAccount { get; set; }
    }
}
