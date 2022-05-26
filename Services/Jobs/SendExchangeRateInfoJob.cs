using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quartz;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Telegram.Bot;

namespace Services.Jobs
{
    public class SendExchangeRateInfoJob : IJob
    {
        private readonly TelegramBotClient telegramBotClient;
        private readonly HttpClient client;
        private readonly IApplicationDbContext context;

        public SendExchangeRateInfoJob(IConfiguration config, IApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            this.telegramBotClient = new TelegramBotClient(config["token"]);
            this.client = httpClientFactory.CreateClient();
            this.context = context;
            this.client.DefaultRequestHeaders.Add("apikey", config["APIKY:key"]);
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var subsciptions = this.context.Subscriptions.Include(s => s.User).AsEnumerable();
            foreach(var subscription in subsciptions)
            {
                var response = await this.client.GetAsync($"https://api.apilayer.com/exchangerates_data/convert?to={subscription.ToCurrency}&from={subscription.FromCurrency}&amount=1");
                var result = await response.Content.ReadFromJsonAsync<CurrencyInfo>();
                var responseText = await response.Content.ReadAsStringAsync();
                var text = $"1 {subscription.FromCurrency} = {result.Result} {subscription.ToCurrency}";
                var subscriber = subscription.User;
                await this.telegramBotClient.SendTextMessageAsync(subscriber.TelegramId, text);
            }
            Console.WriteLine("Job is executed!");
        }

        class CurrencyInfo
        {
            [JsonPropertyName("result")]
            public decimal Result { get; set; }
        }
    }
}
