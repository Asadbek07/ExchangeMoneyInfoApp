using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace ExchangeMoneyInfoApp.Api.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class TgBotController : ControllerBase
    {
        private readonly ITelegramService telegramService;

        public TgBotController(ITelegramService telegramService)
        {
            this.telegramService = telegramService;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] object update)
        {
            var upd = JsonConvert.DeserializeObject<Update>(update.ToString());

            try
            {
                await this.telegramService.ExecuteAsync(upd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
