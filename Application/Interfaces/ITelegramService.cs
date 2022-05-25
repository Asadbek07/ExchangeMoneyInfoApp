
using Telegram.Bot.Types;

namespace Application.Interfaces
{
    public interface ITelegramService
    {
        public Task ExecuteAsync(Update update);
    }
}
