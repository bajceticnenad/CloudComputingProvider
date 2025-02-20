namespace CloudComputingProvider.BusinessModel.ResponseModels
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SoftwareId { get; set; }
        public int CustomerAccountId { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderLicence> OrderLicences { get; set; }
    }
}
