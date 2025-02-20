namespace CloudComputingProvider.BusinessModel.ResponseModels
{
    public class SubscriptionDetailsResponse
    {
        public int Id { get; set; }
        public int LicenceId { get; set; }
        public string Licence { get; set; }
        public States State { get; set; }
        public DateTime ValidToDate { get; set; }
    }
}
