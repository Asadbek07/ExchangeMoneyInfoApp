using Application.Features.SendRecurringInfoFeature.Commands;
using Application.Features.SendRecurringInfoFeature.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeMoneyInfoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendRecurringInfoController : ControllerBase
    {
        private readonly IMediator mediator;
        public SendRecurringInfoController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost]
        public async Task<ActionResult<SendRecurringInfoCommandResponse>> PostAsync(
            [FromBody]SendRecurringInfoCommandRequest request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<GetCurrencySymbolsListQueryResponse>> GetListOfCurrencySymbolsAsync(
            [FromQuery] GetCurrencySymbolsListQueryRequest request)
        {
            var response = await this.mediator.Send(request);
            
            return Ok(response);
        }

        [HttpGet("/convert")]
        public async Task<ActionResult<GetCurrencyExchangeRateQueryResponse>> GetListOfCurrencyExchangeRateAsync(
            [FromQuery] string FromCurrency,
            [FromQuery] string ToCurrency)
        {
            var response = await this.mediator.Send(new GetCurrencyExchangeRateQueryRequest { FromCurrency = FromCurrency, ToCurrency = ToCurrency});

            return Ok(response);
        }
    }
}
