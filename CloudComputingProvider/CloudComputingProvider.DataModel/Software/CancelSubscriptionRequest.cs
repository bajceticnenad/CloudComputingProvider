namespace CloudComputingProvider.DataModel.Software
{
    public class CancelSubscriptionRequest
    {
        public int OrderId { get; set; }
        public int SoftwareId { get; set; }
    }
}
