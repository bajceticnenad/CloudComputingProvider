using System.ComponentModel.DataAnnotations;

namespace CloudComputingProvider.DataModel.Domain.Models
{
    public class Customers : BaseEntity<int>
    {
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string TaxIdentificationNumber { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public ICollection<CustomerAccounts> CustomerAccounts { get; set; }
    }
}
