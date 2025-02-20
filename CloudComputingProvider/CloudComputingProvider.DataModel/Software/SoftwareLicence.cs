using System.ComponentModel.DataAnnotations;

namespace CloudComputingProvider.DataModel.Software
{
    public class SoftwareLicence
    {
        public int LicenceId { get; set; }

        [StringLength(250)]
        public string Licence { get; set; }

        public int SoftwareId { get; set; }
        public DateTime ValidToDate { get; set; }
    }
}
