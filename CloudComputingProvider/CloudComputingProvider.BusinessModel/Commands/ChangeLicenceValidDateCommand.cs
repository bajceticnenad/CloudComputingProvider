using MediatR;

namespace CloudComputingProvider.BusinessModel.Commands
{
    public class ChangeLicenceValidDateCommand : IRequest<Response>
    {
        public int LicenceId { get; set; }
        public DateTime ValidToDate { get; set; }
    }
}
