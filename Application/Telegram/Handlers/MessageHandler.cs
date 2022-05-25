using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Telegram.States;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Handlers
{
    public static class MessageHandler
    {
        public static async Task Handle(Update update, TelegramBotClient telegramBotClient)
        {
            if (UserStateStorage.Users.ContainsKey(update.Message.From.Id))
                return;

            UserStateStorage.Users[update.Message.From.Id] = new UserInfo();
            var buttons = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Start") { CallbackData = "Start" }
            };

            var inlineKeyboard = new InlineKeyboardMarkup(buttons);
            await telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id,
                "This bot will send currency exchange rates. Click Start."
                , replyMarkup: inlineKeyboard);
        }
    }
}
