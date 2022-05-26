using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SubscribeRecurringInfoFeature.Commands
{
    public class SubscribeRecurringInfoCommandRequest : IRequest<SubscribeRecurringInfoCommandResponse>
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public DateTimeOffset NotificationTime { get; set; }
        public int TelegramId { get; set; }
    }

    public class SubscribeRecurringInfoCommandHandler : IRequestHandler<SubscribeRecurringInfoCommandRequest, SubscribeRecurringInfoCommandResponse>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        public SubscribeRecurringInfoCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<SubscribeRecurringInfoCommandResponse> Handle(SubscribeRecurringInfoCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.TelegramId == request.TelegramId);
            var subscription = this.mapper.Map<Subscription>(request);
            subscription.UserId = user.Id;
            this.context.Subscriptions.Add(subscription);
            await this.context.SaveChangesAsync();
            return new SubscribeRecurringInfoCommandResponse
            {
                Id = subscription.Id
            };
        }
    }
    public class SubscribeRecurringInfoCommandResponse
    {
        public int Id { get; set; }
    }
}
