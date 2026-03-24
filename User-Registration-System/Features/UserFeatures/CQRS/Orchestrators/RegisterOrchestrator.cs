using MediatR;
using User_Registration_System.Features.UserFeatures.CQRS.Commands.CreateUser;
using User_Registration_System.Features.UserFeatures.CQRS.Quries.GetUerById;
using User_Registration_System.Shared.Interfaces;
using User_Registration_System.Shared.Respones;

namespace User_Registration_System.Features.UserFeatures.CQRS.Orchestrators
{
    public record RegisterOrchestrator(string UserName, string UserEmail):IRequest<RequestRespone<bool>>;

    public class RegisterOrchestratorHandler : IRequestHandler<RegisterOrchestrator, RequestRespone<bool>>
    {
        private readonly IMediator mediator;

        public RegisterOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<RequestRespone<bool>> Handle(RegisterOrchestrator request, CancellationToken cancellationToken)
        {
            var GetUserByEmailResult = await mediator.Send(new GetUerByEmailQuery(request.UserEmail));

            if (GetUserByEmailResult.Data!=null)
            {
                return RequestRespone<bool>.Failure("This email is already registered",400);
            }

            var CreateUserResult = await mediator.Send(new CreateUserCommand(request.UserName,request.UserEmail));

            if (!CreateUserResult.IsSuccess)
            {
                return RequestRespone<bool>.Failure(CreateUserResult.Message, CreateUserResult.StatusCode);

            }

            return RequestRespone<bool>.Success(true, "Registration completed successfully!", 201);
        }
    }
}
