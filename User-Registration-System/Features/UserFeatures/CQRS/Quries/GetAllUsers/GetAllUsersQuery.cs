using MediatR;
using Microsoft.EntityFrameworkCore;
using User_Registration_System.Features.UserFeatures.CQRS.Quries.DTOs;
using User_Registration_System.Shared.Entities;
using User_Registration_System.Shared.Interfaces;
using User_Registration_System.Shared.Respones;

namespace User_Registration_System.Features.UserFeatures.CQRS.Quries.GetAllUsers
{
    public record GetAllUsersQuery:IRequest<RequestRespone<IEnumerable<UserToReturnDto>>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, RequestRespone<IEnumerable<UserToReturnDto>>>
    {
        private readonly IGenericRepository<User> UserRepository;

        public GetAllUsersQueryHandler(IGenericRepository<User> UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public async Task<RequestRespone<IEnumerable<UserToReturnDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var Users = await UserRepository.GetAll().Select(x => new UserToReturnDto
            {
                Userid = x.Id,
                UserName = x.Name,
                UserEmail = x.Email,
                CreatedAt= x.CreatedAt
            }).ToListAsync(cancellationToken);

            if (Users == null || !Users.Any())
            {
                return RequestRespone<IEnumerable<UserToReturnDto>>.Failure("No users found in the system", 404);
            }

            return RequestRespone<IEnumerable<UserToReturnDto>>.Success(Users, "Users retrieved successfully", 200);

        }
    }
}
