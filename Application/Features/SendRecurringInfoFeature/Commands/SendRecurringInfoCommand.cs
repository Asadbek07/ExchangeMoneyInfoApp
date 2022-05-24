using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SendRecurringInfoFeature.Commands
{
    public class SendRecurringInfoCommandRequest : IRequest<SendRecurringInfoCommandResponse>
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public DateTimeOffset NotificationTime { get; set; }
        public int TelegramId { get; set; }
    }

    public class SendRecurringInfoCommandHandler : IRequestHandler<SendRecurringInfoCommandRequest, SendRecurringInfoCommandResponse>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        public SendRecurringInfoCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<SendRecurringInfoCommandResponse> Handle(SendRecurringInfoCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.TelegramId == request.TelegramId);
            var subscription = this.mapper.Map<Subscription>(request);
            subscription.UserId = user.Id;
            this.context.Subscriptions.Add(subscription);
            await this.context.SaveChangesAsync();
            return new SendRecurringInfoCommandResponse
            {
                Id = subscription.Id
            };
        }
    }
    public class SendRecurringInfoCommandResponse
    {
        public int Id { get; set; }
    }
}
