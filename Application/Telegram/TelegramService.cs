using Application.Interfaces;
using Application.Telegram.Handlers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Application.Telegram
{
    public class TelegramService : ITelegramService
    {
        private readonly TelegramBotClient telegramBotClient;
        private readonly IMediator _mediator;

        public TelegramService(IConfiguration configuration, IMediator mediator)
        {
            this.telegramBotClient = new TelegramBotClient("5388725901:AAF0dpazNT0hjUoR3rY6V7eyOOEHAqkej5k");
            this._mediator = mediator;
        }
        public async Task ExecuteAsync(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await MessageHandler.Handle(update, telegramBotClient);
                    break;
                default:
                    await CallbackQueryHandler.Handle(update, telegramBotClient, _mediator);
                    break;
            }
        }
    }
}
