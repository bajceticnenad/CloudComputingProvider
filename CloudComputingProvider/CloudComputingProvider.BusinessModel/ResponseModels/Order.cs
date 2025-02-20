namespace CloudComputingProvider.BusinessModel.ResponseModels
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
        public string Description { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
