using System.ComponentModel.DataAnnotations;

namespace CloudComputingProvider.DataModel.Domain.Models
{
    public class SubscriptionDetails : BaseEntity<int>
    {
        public int SubscriptionId { get; set; }
        public Subscriptions Subscription { get; set; }

        public int LicenceId { get; set; }

        [StringLength(250)]
        public string Licence { get; set; }

        public DateTime ValidToDate { get; set; }
    }
}
