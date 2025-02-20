namespace CloudComputingProvider.DataModel.Order
{
    public class OrderLicence
    {
        public int OrderItemId { get; set; }
        public int LicenceId { get; set; }
        public string Licence { get; set; }
        public DateTime ValidToDate { get; set; }
    }
}
