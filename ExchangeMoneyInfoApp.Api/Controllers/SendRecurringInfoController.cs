using Application.Features.SendRecurringInfoFeature.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeMoneyInfoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendRecurringInfoController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public SendRecurringInfoController(IMediator mediator)
        {
            mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost]
        public async Task<ActionResult<SendRecurringInfoCommandResponse>> PostAsync(
            SendRecurringInfoCommandRequest request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
