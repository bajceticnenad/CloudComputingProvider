using System.ComponentModel.DataAnnotations;

namespace CloudComputingProvider.DataModel.Domain.Models
{
    public class Subscriptions : BaseEntity<int>
    {
        public int CustomerAccountId { get; set; }
        public CustomerAccounts CustomerAccount { get; set; }

        public int SoftwareId { get; set; }
        
        [StringLength(250)]
        public string SoftwareName { get; set; }
        
        public int Quantity { get; set; }

        public int StateId { get; set; }
        public States State { get; set; }

        public int OrderId { get; set; }

        public ICollection<SubscriptionDetails> SubscriptionDetails { get; set; }
    }
}
