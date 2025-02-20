using AutoMapper;
using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.BusinessModel.Commands;
using CloudComputingProvider.BusinessModel.ResponseModels;
using CloudComputingProvider.DataModel.Domain.Models;
using CloudComputingProvider.DataModel.Software;
using CloudComputingProvider.Infrastructure.Interfaces.Repositories;
using CloudComputingProvider.Services.Interfaces.APIs;
using MediatR;
using Microsoft.Extensions.Logging;
using DM = CloudComputingProvider.DataModel;

namespace CloudComputingProvider.BusinessLogic.Handlers.Commands
{
    public class ChangeLicencesQuantityHandler : IRequestHandler<ChangeLicencesQuantityCommand, Response<List<ChangeLicencesQuantityResponse>>>
    {
        #region PrivateFields
        private readonly ILogger<ChangeLicencesQuantityHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly ICcpOrderService _ccpOrderService;

        #endregion PrivateFields

        #region Constructor
        public ChangeLicencesQuantityHandler(ILogger<ChangeLicencesQuantityHandler> logger, IMapper mapper,
            ISubscriptionsRepository subscriptionsRepository, ICcpOrderService ccpOrderService)
        {
            _logger = logger;
            _mapper = mapper;
            _subscriptionsRepository = subscriptionsRepository;
            _ccpOrderService = ccpOrderService;
        }
        #endregion Constructor

        #region PublicMethods
        public async Task<Response<List<ChangeLicencesQuantityResponse>>> Handle(ChangeLicencesQuantityCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Logger: {nameof(ChangeLicencesQuantityHandler)}/{nameof(Handle)}");

            var response = new Response<List<ChangeLicencesQuantityResponse>>()
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

            switch ((command.Quantity, subscription.Quantity))
            {
                case var s when command.Quantity > subscription.Quantity:
                    response = await AddNewLicences(subscription, command, cancellationToken);
                    break;
                case var s when command.Quantity < subscription.Quantity:
                    response = await RemoveExistingLicences(subscription, command, cancellationToken);
                    break;
                default:
                    response.Success = false;
                    response.ResponseMessage = $"Current quantity and new quantity are equal!";
                    break;
            }

            return response;
        }
        #endregion PublicMethods

        #region PrivateMethods
        private async Task<Response<List<ChangeLicencesQuantityResponse>>> AddNewLicences(Subscriptions subscription, ChangeLicencesQuantityCommand command, CancellationToken cancellationToken)
        {
            var response = new Response<List<ChangeLicencesQuantityResponse>>()
            {
                Success = true
            };

            //call ccp_order api to make a order for newly add licences
            var quantity = command.Quantity - subscription.Quantity;
            var addSubscriptionLicenceRequest = new AddNewSubscriptionLicenceRequest()
            {
                OrderId = subscription.OrderId,
                SoftwareId = subscription.SoftwareId,
                Quantity = quantity
            };

            var newLicencseOrderResponse = await _ccpOrderService.AddNewSubscriptionLicence(addSubscriptionLicenceRequest);
            if (!newLicencseOrderResponse.Success)
            {
                response.Success = newLicencseOrderResponse.Success;
                response.ResponseMessage = newLicencseOrderResponse.ResponseMessage;
                return response;
            }

            var subscriptionDetails = _mapper.Map<List<SubscriptionDetails>>(newLicencseOrderResponse.Data);

            var result = await _subscriptionsRepository.AddNewSubscriptionLicences(subscription.Id, command.Quantity, subscriptionDetails, cancellationToken);
            if (result == 0)
            {
                response.Success = false;
                response.ResponseMessage = $"Subscription Licences is not added properly!";
                return response;
            }

            return response;
        }

        private async Task<Response<List<ChangeLicencesQuantityResponse>>> RemoveExistingLicences(Subscriptions subscription, ChangeLicencesQuantityCommand command, CancellationToken cancellationToken)
        {
            var response = new Response<List<ChangeLicencesQuantityResponse>>()
            {
                Success = true
            };

            var deletionQuantity = subscription.Quantity - command.Quantity;

            var subscriptionDetails = subscription.SubscriptionDetails.TakeLast(deletionQuantity);
            if (!subscriptionDetails.Any())
            {
                response.Success = false;
                response.ResponseMessage = $"For subscription id: {subscription.Id} does not exist any licence!";
                return response;
            }

            //call ccp_order api to unsubscribe licences in CCP system
            var cancelSubscriptionLicenceRequest = new CancelSubscriptionLicenceRequest()
            {
                OrderId = subscription.OrderId,
                SoftwareId = subscription.SoftwareId,
                SoftwareLicences = new List<DM.Software.SoftwareLicence>()
            };
            cancelSubscriptionLicenceRequest.SoftwareLicences = _mapper.Map<List<DM.Software.SoftwareLicence>>(subscriptionDetails);

            var cancelSubscriptionResponse = await _ccpOrderService.CancelSubscriptionLicence(cancelSubscriptionLicenceRequest);
            if (!cancelSubscriptionResponse.Success)
            {
                response.Success = cancelSubscriptionResponse.Success;
                response.ResponseMessage = cancelSubscriptionResponse.ResponseMessage;
                return response;
            }

            var deleteResult = await _subscriptionsRepository.RemoveSubscriptionLicences(subscription.Id, command.Quantity, subscriptionDetails, cancellationToken);
            if (deleteResult == 0)
            {
                response.Success = false;
                response.ResponseMessage = $"Subscription is not updated properly: {subscription.Id}";
                return response;
            }

            return response;
        }

        #endregion PrivateMethods
    }
}
