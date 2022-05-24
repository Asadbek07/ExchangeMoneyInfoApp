using Application.DTO;
using MediatR;
using System.Text.Json;

namespace Application.Features.SendRecurringInfoFeature.Queries
{
    public class GetCurrencySymbolsListQueryRequest : IRequest<GetCurrencySymbolsListQueryResponse>
    {
        public string? CurrencySymbol { get; set; }
    }

    public class GetCurrencySymbolsListQueryHandler : IRequestHandler<GetCurrencySymbolsListQueryRequest, GetCurrencySymbolsListQueryResponse>
    {
        public async Task<GetCurrencySymbolsListQueryResponse> Handle(GetCurrencySymbolsListQueryRequest request, CancellationToken cancellationToken)
        {
            var symbols = new Currencies();
            var properties = symbols.GetType().GetProperties();
            var currencySymbols = new List<string>();
                
            for (int i = 0; i < 10; i++)
            {
                currencySymbols.Add(properties[i].Name);
            }
            if (request.CurrencySymbol is not null)
            {
                currencySymbols.Remove(request.CurrencySymbol.ToUpper());
            }

            return new GetCurrencySymbolsListQueryResponse
            {
                CurrentcySymbols = currencySymbols
            };
            
        }
    }

    public class GetCurrencySymbolsListQueryResponse
    {
        public List<string> CurrentcySymbols { get; set; }
    }


}
