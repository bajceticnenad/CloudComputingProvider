using AutoMapper;
using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.BusinessModel.Commands;
using CloudComputingProvider.DataModel.Software;
using CloudComputingProvider.Infrastructure.Interfaces.Repositories;
using CloudComputingProvider.Services.Interfaces.APIs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CloudComputingProvider.BusinessLogic.Handlers.Commands
{
    public class ChangeLicenceValidDateHandler : IRequestHandler<ChangeLicenceValidDateCommand, Response>
    {
        #region PrivateFields
        private readonly ILogger<ChangeLicenceValidDateHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly ICcpOrderService _ccpOrderService;

        #endregion PrivateFields

        #region Constructor
        public ChangeLicenceValidDateHandler(ILogger<ChangeLicenceValidDateHandler> logger, IMapper mapper,
            ISubscriptionsRepository subscriptionsRepository, ICcpOrderService ccpOrderService)
        {
            _logger = logger;
            _mapper = mapper;
            _subscriptionsRepository = subscriptionsRepository;
            _ccpOrderService = ccpOrderService;
        }
        #endregion Constructor

        #region PublicMethods
        public async Task<Response> Handle(ChangeLicenceValidDateCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Logger: {nameof(ChangeLicenceValidDateHandler)}/{nameof(Handle)}");

            var response = new Response()
            {
                Success = true
            };

            var subscriptionDetails = await _subscriptionsRepository.GetLicenceById(command.LicenceId);
            if (subscriptionDetails == null)
            {
                response.Success = false;
                response.ResponseMessage = $"Licence with Id={command.LicenceId} does not exist!";
                return response;
            }

            if (command.ValidToDate <= subscriptionDetails.ValidToDate)
            {
                response.Success = false;
                response.ResponseMessage = $"New Licence valid to date must be greater then current date!";
                return response;
            }

            //Extend licence valid to date in CCP system
            var extendLicenceValidDateRequest = _mapper.Map<ExtendLicenceValidDateRequest>(command);

            var extendLicenceValidDate = await _ccpOrderService.ExtendLicenceValidDate(extendLicenceValidDateRequest);
            if (!extendLicenceValidDate.Success)
            {
                response.Success = extendLicenceValidDate.Success;
                response.ResponseMessage = extendLicenceValidDate.ResponseMessage;
                return response;
            }

            subscriptionDetails.ValidToDate = command.ValidToDate;

            var result = await _subscriptionsRepository.ModifySubscriptionDetails(subscriptionDetails, cancellationToken);
            if (result == 0)
            {
                response.Success = false;
                response.ResponseMessage = $"Subscription Details is not updated properly: {subscriptionDetails.Id}";
                return response;
            }

            return response;
        }
        #endregion PublicMethods
    }
}
