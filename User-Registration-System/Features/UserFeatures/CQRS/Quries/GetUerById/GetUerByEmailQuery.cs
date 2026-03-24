using MediatR;
using Microsoft.EntityFrameworkCore;
using User_Registration_System.Features.UserFeatures.CQRS.Quries.DTOs;
using User_Registration_System.Shared.Entities;
using User_Registration_System.Shared.Interfaces;
using User_Registration_System.Shared.Respones;

namespace User_Registration_System.Features.UserFeatures.CQRS.Quries.GetUerById
{
    public record GetUerByEmailQuery(string UserEmail):IRequest<RequestRespone<UserToReturnDto>>;

    public class GetUerByIdQueryHandler : IRequestHandler<GetUerByEmailQuery, RequestRespone<UserToReturnDto>>
    {
        private readonly IGenericRepository<User> UserRepository;

        public GetUerByIdQueryHandler(IGenericRepository<User> UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public async Task<RequestRespone<UserToReturnDto>> Handle(GetUerByEmailQuery request, CancellationToken cancellationToken)
        {
            var User = await UserRepository.GetByCriteriaAsync(x => x.Email == request.UserEmail).Select(x=> new UserToReturnDto 
            {
                Userid=x.Id,
                UserEmail=request.UserEmail,
            }).FirstOrDefaultAsync(cancellationToken);

            if (User == null)
            {
                return RequestRespone<UserToReturnDto>.Failure("User with this email not found", 404);
            }

            return RequestRespone<UserToReturnDto>.Success(User, "User found successfully.", 200);

        }
    }
}
