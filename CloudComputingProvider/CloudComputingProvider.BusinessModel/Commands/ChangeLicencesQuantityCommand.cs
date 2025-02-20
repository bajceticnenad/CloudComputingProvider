using CloudComputingProvider.BusinessModel.ResponseModels;
using MediatR;

namespace CloudComputingProvider.BusinessModel.Commands
{
    public class ChangeLicencesQuantityCommand : IRequest<Response<List<ChangeLicencesQuantityResponse>>>
    {
        public int SubscriptionId { get; set; }
        public int Quantity { get; set; }
    }
}
