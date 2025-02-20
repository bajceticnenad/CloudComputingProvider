using AutoMapper;
using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.BusinessModel.Commands;
using CloudComputingProvider.BusinessModel.Enums;
using CloudComputingProvider.DataModel.Software;
using CloudComputingProvider.Infrastructure.Interfaces.Repositories;
using CloudComputingProvider.Services.Interfaces.APIs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CloudComputingProvider.BusinessLogic.Handlers.Commands
{
    public class SubscriptionCancelHandler : IRequestHandler<SubscriptionCancelCommand, Response>
    {
        #region PrivateFields
        private readonly ILogger<SubscriptionCancelHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly ICcpOrderService _ccpOrderService;

        #endregion PrivateFields

        #region Constructor
        public SubscriptionCancelHandler(ILogger<SubscriptionCancelHandler> logger, IMapper mapper, ISubscriptionsRepository subscriptionsRepository, 
            ICcpOrderService ccpOrderService)
        {
            _logger = logger;
            _mapper = mapper;
            _subscriptionsRepository = subscriptionsRepository;
            _ccpOrderService = ccpOrderService;
        }
        #endregion Constructor

        #region PublicMethods
        public async Task<Response> Handle(SubscriptionCancelCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Logger: {nameof(SubscriptionCancelHandler)}/{nameof(Handle)}");

            var response = new Response()
            {
                Success = true
            };

            var subscription = await _subscriptionsRepository.GetSubscriptionById(command.SubscriptionId);
            if (subscription == null)
            {
                response.Success = false;
                response.ResponseMessage = $"Subscription with Id={command.SubscriptionId} does not exist!";
                return response;
            }

            //Cancel Subscription in CCP system.
            var cancelSubscriptionRequest = new CancelSubscriptionRequest()
            {
                OrderId = subscription.OrderId,
                SoftwareId = subscription.SoftwareId
            };

            var cancelSubscriptionResponse = await _ccpOrderService.CancelSubscription(cancelSubscriptionRequest);
            if (!cancelSubscriptionResponse.Success)
            {
                response.Success = cancelSubscriptionResponse.Success;
                response.ResponseMessage = cancelSubscriptionResponse.ResponseMessage;
                return response;
            }

            //if subscription cancelled succesifully, we can cancel subscription in database
            subscription.StateId = (int)State.Cancelled;

            var result = await _subscriptionsRepository.ModifySubscription(subscription, cancellationToken);
            if (result == 0)
            {
                response.Success = false;
                response.ResponseMessage = $"Subscription is not updated properly: {subscription.Id}";
                return response;
            }

            return response;
        }
        #endregion PublicMethods
    }
}
