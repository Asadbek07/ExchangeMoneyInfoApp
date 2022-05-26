using Application.Features.SubscribeRecurringInfoFeature.Commands;
using AutoMapper;
using Domain.Models;

namespace Application.Features.SubscribeRecurringInfoFeature
{
    public class SendRecurringInfoProfile : Profile
    {
        public SendRecurringInfoProfile()
        {
            CreateMap<SubscribeRecurringInfoCommandRequest, Subscription>();
        }
    }
}
