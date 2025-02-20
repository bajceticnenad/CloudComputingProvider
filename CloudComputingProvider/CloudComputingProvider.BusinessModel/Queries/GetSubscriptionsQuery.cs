using CloudComputingProvider.BusinessModel.ResponseModels;
using MediatR;

namespace CloudComputingProvider.BusinessModel.Queries
{
    public class GetSubscriptionsQuery : IRequest<Response<List<SubscriptionsResponse>>>
    {
        public int CustomerAccountId { get; set; }
    }
}
