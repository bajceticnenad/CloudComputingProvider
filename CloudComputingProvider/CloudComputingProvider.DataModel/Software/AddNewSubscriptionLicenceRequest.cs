namespace CloudComputingProvider.DataModel.Software
{
    public class AddNewSubscriptionLicenceRequest
    {
        public int OrderId { get; set; }
        public int SoftwareId { get; set; }
        public int Quantity { get; set; }
    }
}
