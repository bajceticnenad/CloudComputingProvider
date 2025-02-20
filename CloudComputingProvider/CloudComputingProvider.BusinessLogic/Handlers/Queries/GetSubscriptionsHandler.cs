using AutoMapper;
using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.BusinessModel.Queries;
using CloudComputingProvider.BusinessModel.ResponseModels;
using CloudComputingProvider.Infrastructure.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CloudComputingProvider.BusinessLogic.Handlers.Queries
{
    public class GetSubscriptionsHandler : IRequestHandler<GetSubscriptionsQuery, Response<List<SubscriptionsResponse>>>
    {
        #region PrivateFields
        private readonly ILogger<GetSubscriptionsHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        #endregion PrivateFields

        #region Constructor
        public GetSubscriptionsHandler(ILogger<GetSubscriptionsHandler> logger, IMapper mapper, ISubscriptionsRepository subscriptionsRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _subscriptionsRepository = subscriptionsRepository;
        }
        #endregion Constructor

        #region PublicMethods
        public async Task<Response<List<SubscriptionsResponse>>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Logger: {nameof(GetSubscriptionsHandler)}/{nameof(Handle)}");

            var response = new Response<List<SubscriptionsResponse>>()
            {
                Data = new List<SubscriptionsResponse>(),
                Success = true
            };

            var subscriptions = await _subscriptionsRepository.GetSubscriptions(request.CustomerAccountId, cancellationToken);
            if (subscriptions.Any())
            {
                response.Data = _mapper.Map<List<SubscriptionsResponse>>(subscriptions);
            }

            return response;
        }
        #endregion PublicMethods
    }
}
