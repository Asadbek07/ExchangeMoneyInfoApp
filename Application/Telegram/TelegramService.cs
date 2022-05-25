using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Application.Telegram
{
    public class TelegramService : ITelegramService
    {
        private readonly TelegramBotClient telegramBotClient;

        public TelegramService(IConfiguration configuration)
        {
            this.telegramBotClient = new TelegramBotClient(configuration["token"]);
        }
        public async Task ExecuteAsync(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await this.telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, update.Message.Text);
                    break;
                default:
                    await this.telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, update.Message.Text);
                    break;
            }
        }
    }
}
