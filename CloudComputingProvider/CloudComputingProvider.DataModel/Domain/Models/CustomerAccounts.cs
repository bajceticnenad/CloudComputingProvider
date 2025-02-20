using System.ComponentModel.DataAnnotations;

namespace CloudComputingProvider.DataModel.Domain.Models
{
    public class CustomerAccounts : BaseEntity<int>
    {
        [StringLength(50)]
        public string AccountNo { get; set; }
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
