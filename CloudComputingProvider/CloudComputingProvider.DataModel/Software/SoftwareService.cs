using System.ComponentModel.DataAnnotations;

namespace CloudComputingProvider.DataModel.Software
{
    public class SoftwareService
    {
        public int SoftwareId { get; set; }

        [StringLength(250)]
        public string SoftwareName { get; set; }

        public int Quantity { get; set; }

        public List<SoftwareLicence> SoftwareLicences { get; set; }
    }
}
