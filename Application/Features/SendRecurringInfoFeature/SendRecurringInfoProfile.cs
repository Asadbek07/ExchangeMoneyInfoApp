using Application.Features.SendRecurringInfoFeature.Commands;
using AutoMapper;
using Domain.Models;

namespace Application.Features.SendRecurringInfoFeature
{
    public class SendRecurringInfoProfile : Profile
    {
        public SendRecurringInfoProfile()
        {
            CreateMap<SendRecurringInfoCommandRequest, Subscription>();
        }
    }
}
