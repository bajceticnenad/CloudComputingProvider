using MediatR;

namespace CloudComputingProvider.BusinessModel.Commands
{
    public class SubscriptionCancelCommand : IRequest<Response>
    {
        public int SubscriptionId { get; set; }
    }
}
