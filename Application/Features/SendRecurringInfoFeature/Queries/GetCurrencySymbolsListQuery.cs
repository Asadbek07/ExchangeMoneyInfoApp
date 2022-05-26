using Application.DTO;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Application.Features.SubscribeRecurringInfoFeature.Queries
{
    public class GetCurrencySymbolsListQueryRequest : IRequest<GetCurrencySymbolsListQueryResponse>
    {
        public string? CurrencySymbol { get; set; }
    }

    public class GetCurrencySymbolsListQueryHandler : IRequestHandler<GetCurrencySymbolsListQueryRequest, GetCurrencySymbolsListQueryResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly string _apikey;

        public GetCurrencySymbolsListQueryHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _apikey = configuration["APIKEY:key"];
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", _apikey);
        }

        public async Task<GetCurrencySymbolsListQueryResponse> Handle(GetCurrencySymbolsListQueryRequest request, CancellationToken cancellationToken)
        {
            var symbols = new Currencies();
            var properties = symbols.GetType().GetProperties();
            var currencySymbols = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                currencySymbols.Add(properties[i].Name);
            }
            
            List<ExchangeRateInfo> ExchangeRates = new();

            if (request.CurrencySymbol is not null)
            {
                currencySymbols.Remove(request.CurrencySymbol.ToUpper());
                var httpResponse = await _httpClient.GetAsync($"https://api.apilayer.com/currency_data/live?source={request.CurrencySymbol}&currencies={"USD,EUR,RUB,AED,KWN,KZT,LAK"}");
                httpResponse.EnsureSuccessStatusCode();
                var responseBody = await httpResponse.Content.ReadAsStringAsync();
                var responseModel = JsonSerializer.Deserialize<Root>(responseBody);

                var propertiesOfQuotes = typeof(Quotes).GetProperties();

                var quotes = responseModel.quotes;
                foreach (var property in propertiesOfQuotes)
                {
                    ExchangeRates.Add(new ExchangeRateInfo
                    {
                        FromCurrency = responseModel.source,
                        ToCurrency = property.Name,
                        ExchangeRate = (float)quotes.GetType().GetProperty(property.Name).GetValue(quotes, null)
                    });
                }               
                

            }
            return new GetCurrencySymbolsListQueryResponse
            {
                ExchangeRates = ExchangeRates
            };
            
        }
    }

    public class GetCurrencySymbolsListQueryResponse
    {
        public List<ExchangeRateInfo> ExchangeRates { get; set; }
    }

    public class ExchangeRateInfo
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public float? ExchangeRate { get; set; }
    }


}
