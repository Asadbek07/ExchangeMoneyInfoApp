using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommandRequest: IRequest<CreateUserCommandResponse>
    {
        public long TelegramId { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = this.mapper.Map<User>(request);
            var userEntry = this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
            
            return new CreateUserCommandResponse
            {
                Id = userEntry.Entity.Id,
            };
        }
    }

    public class CreateUserCommandResponse
    {
        public int Id { get; set; }
    }
}
