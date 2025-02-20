using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.BusinessModel.Commands;
using CloudComputingProvider.BusinessModel.Queries;
using CloudComputingProvider.BusinessModel.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CloudComputingProvider.Controllers
{
    /// <summary>
    /// Subscription Controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        #region PrivateFields
        private readonly ILogger<SubscriptionController> _logger;
        private readonly IMediator _mediator;
        #endregion PrivateFields

        #region Constructor
        /// <summary>
        /// Subscription Controller constructor
        /// </summary>
        public SubscriptionController(ILogger<SubscriptionController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        #endregion Constructor

        #region PublicMethods
        /// <summary>
        /// Return purchased software Licenses by Customer Account
        /// </summary>
        [HttpGet("customerAccountId/{customerAccountId}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<SubscriptionsResponse>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSubscriptions([FromRoute] int customerAccountId)
        {
            _logger.LogDebug($"Logger Test: {nameof(SubscriptionController)}/{nameof(GetSubscriptions)}");
            var query = new GetSubscriptionsQuery
            {
                CustomerAccountId = customerAccountId
            };
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Extend the software license valid to date
        /// </summary>
        [HttpPost("ChangeLicenceValidDate")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangeLicenceValidDate([FromBody] ChangeLicenceValidDateCommand command)
        {
            _logger.LogDebug($"Logger Test: {nameof(SubscriptionController)}/{nameof(ChangeLicenceValidDate)}");

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Cancel the specific software under any account
        /// </summary>
        [HttpPut("Cancel/{subscriptionId}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Cancel([FromRoute] int subscriptionId)
        {
            _logger.LogDebug($"Logger Test: {nameof(SubscriptionController)}/{nameof(Cancel)}");

            var command = new SubscriptionCancelCommand
            {
                SubscriptionId = subscriptionId
            };
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Change quantity of service licenses per subscription
        /// </summary>
        [HttpPost("ChangeLicencesQuantity")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangeLicencesQuantity([FromBody] ChangeLicencesQuantityCommand command)
        {
            _logger.LogDebug($"Logger Test: {nameof(SubscriptionController)}/{nameof(ChangeLicencesQuantity)}");

            return Ok(await _mediator.Send(command));
        }
        #endregion PublicMethods
    }
}
