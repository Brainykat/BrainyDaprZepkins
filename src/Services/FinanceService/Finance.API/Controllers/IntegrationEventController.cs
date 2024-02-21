using Dapr;
using EventBus.Dtos;
using Finance.Services.IntegrationEvents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationEventController : ControllerBase
    {
        private const string DAPR_PUBSUB_NAME = "brainykatdapr-pubsub";

        [HttpPost("NewCustomerCreated")]
        [Topic(DAPR_PUBSUB_NAME, nameof(CustomerEBDto))]
        public Task HandleAsync(
            CustomerEBDto @event,
            [FromServices] NewCustomerCreatedIntegrationEventHandler handler)
            => handler.Handle(@event);
    }
}
