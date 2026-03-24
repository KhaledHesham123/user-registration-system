using MediatR;
using User_Registration_System.Shared.Entities;
using User_Registration_System.Shared.Interfaces;
using User_Registration_System.Shared.Respones;

namespace User_Registration_System.Features.UserFeatures.CQRS.Commands.CreateUser
{
    public record CreateUserCommand(string UserName, string UserEmail):ICommand<RequestRespone<bool>>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, RequestRespone<bool>>
    {
        private readonly IGenericRepository<User> UserRepository;

        public CreateUserCommandHandler(IGenericRepository<User> UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public async Task<RequestRespone<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var User = new User
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Email = request.UserEmail,
                Name = request.UserName,
            };

            await UserRepository.AddAsync(User);

            int result = await UserRepository.SaveChangesAsync();

            if (result > 0)
            {
                return RequestRespone<bool>.Success(true, "User created successfully", 201);
            }

            return RequestRespone<bool>.Failure("Failed to save user to database", 500);


        }
    }
}
