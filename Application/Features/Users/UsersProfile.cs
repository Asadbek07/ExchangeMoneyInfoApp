using Application.Features.Users.Commands;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Users
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<CreateUserCommandRequest, User>();
        }
    }
}
