using AutoMapper;
using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.BusinessModel.Commands;
using CloudComputingProvider.BusinessModel.ResponseModels;
using CloudComputingProvider.Infrastructure.Interfaces.Repositories;
using CloudComputingProvider.Services.Interfaces.APIs;
using MediatR;
using Microsoft.Extensions.Logging;
using DM = CloudComputingProvider.DataModel;

namespace CloudComputingProvider.BusinessLogic.Handlers.Commands
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Response<Order>>
    {
        #region PrivateFields
        private readonly ILogger<CreateOrderHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICcpOrderService _ccpOrderService;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        #endregion PrivateFields

        #region Constructor
        public CreateOrderHandler(ILogger<CreateOrderHandler> logger, IMapper mapper, ICcpOrderService ccpOrderService,
            ISubscriptionsRepository subscriptionsRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _ccpOrderService = ccpOrderService;
            _subscriptionsRepository = subscriptionsRepository;
        }
        #endregion Constructor

        #region PublicMethods
        public async Task<Response<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Logger: {nameof(CreateOrderHandler)}/{nameof(Handle)}");

            var response = new Response<Order>()
            {
                Success = true
            };

            var dm_request = _mapper.Map<DM.Order.CreateOrderRequest>(command);

            //Create order in CCP system. Each order item is one subscription in our system.
            var result = await _ccpOrderService.CreateOrder(dm_request);
            if (!result.Success)
            {
                response.Success = result.Success;
                response.ResponseMessage = result.ResponseMessage;
                return response;
            }

            var subscriptionList = _mapper.Map<List<DM.Domain.Models.Subscriptions>>(result.Data.OrderItems);
            var subscriptionResult = await _subscriptionsRepository.AddSubscriptionList(subscriptionList, cancellationToken);
            if (subscriptionResult == 0)
            {
                response.Success = false;
                response.ResponseMessage = $"Subscriptions were not correctly saved in the database.";
                return response;
            }

            response.Data = _mapper.Map<Order>(result.Data);

            return response;
        }
        #endregion PublicMethods
    }
}
