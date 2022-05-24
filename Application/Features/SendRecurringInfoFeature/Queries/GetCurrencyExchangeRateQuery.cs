using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Features.SendRecurringInfoFeature.Queries
{
    public class GetCurrencyExchangeRateQueryRequest : IRequest<GetCurrencyExchangeRateQueryResponse>
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
    }

    public class GetCurrencyExchangeRateQueryHandler : IRequestHandler<GetCurrencyExchangeRateQueryRequest, GetCurrencyExchangeRateQueryResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly string _apikey = "uHnC6f1zjtWiQbIyjUAcdu48L2uoom8V";

        public GetCurrencyExchangeRateQueryHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", _apikey);
        }

        public async Task<GetCurrencyExchangeRateQueryResponse> Handle(GetCurrencyExchangeRateQueryRequest request, CancellationToken cancellationToken)
        {
            var httpResponse = await _httpClient.GetAsync($"https://api.apilayer.com/currency_data/convert?to={request.ToCurrency}&from={request.FromCurrency}&amount=1");
            httpResponse.EnsureSuccessStatusCode();

            
            var responseBody =
                await httpResponse.Content.ReadAsStringAsync();

            var exchangeRateResponse = JsonSerializer.Deserialize<Rootobject>(responseBody);
            return new GetCurrencyExchangeRateQueryResponse
            {
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                ExchangeRate = exchangeRateResponse.result
            };         
        }
    }

    public class GetCurrencyExchangeRateQueryResponse
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public float ExchangeRate { get; set; }
    }
}
