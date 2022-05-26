using Application.Features.SubscribeRecurringInfoFeature.Queries;
using Application.Telegram.States;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Handlers
{
    public static class CallbackQueryHandler
    {
        public static List<string> currencies = new List<string>
        {
            "USD",
            "UZS"
        };

        public static async Task Handle(Update update, TelegramBotClient telegramBotClient, IMediator mediator)
        {
            var userInfo = UserStateStorage.Users[update.CallbackQuery.From.Id];

            if (userInfo.CurrentState == UserState.ProcessNotStarted)
            {
                userInfo.CurrentState = UserState.SelectingFromCurrency;
                // send inline buttons
                var buttons = new List<InlineKeyboardButton>();
                foreach (var currency in currencies)
                {
                    buttons.Add(new InlineKeyboardButton(currency) { CallbackData = currency});
                }

                var inlineKeyboard = new InlineKeyboardMarkup(buttons);
                await telegramBotClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                    "Select 1-st currency"
                    , replyMarkup: inlineKeyboard);
                // *********
                return;
            }

            if (userInfo.CurrentState == UserState.SelectingFromCurrency)
            {
                userInfo.CurrentState = UserState.SelectingToCurrency;
                userInfo.FromCurrency = update.CallbackQuery.Data;

                // send inline buttons
                var buttons = new List<InlineKeyboardButton>();
                foreach (var currency in currencies)
                {
                    buttons.Add(new InlineKeyboardButton(currency) { CallbackData = currency });
                }

                var inlineKeyboard = new InlineKeyboardMarkup(buttons);
                await telegramBotClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                    "Select 2-nd currency"
                    , replyMarkup: inlineKeyboard);
                // *********
                return;
            }

            if (userInfo.CurrentState == UserState.SelectingToCurrency)
            {
                userInfo.CurrentState = UserState.ProcessNotStarted;
                userInfo.ToCurrency = update.CallbackQuery.Data;

                var response = await mediator.Send(new GetCurrencyExchangeRateQueryRequest()
                {
                    FromCurrency = userInfo.FromCurrency,
                    ToCurrency = userInfo.ToCurrency,
                });

                await telegramBotClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                    $"Exchange Rate for {userInfo.FromCurrency} - {userInfo.ToCurrency} is {response.ExchangeRate}");
                return;
            }
        }
    }
}
