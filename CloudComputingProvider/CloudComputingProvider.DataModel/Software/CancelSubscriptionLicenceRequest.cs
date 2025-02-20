namespace CloudComputingProvider.DataModel.Software
{
    public class CancelSubscriptionLicenceRequest
    {
        public int OrderId { get; set; }
        public int SoftwareId { get; set; }
        public List<SoftwareLicence> SoftwareLicences { get; set; }
    }
}
