using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_Registration_System.Features.UserFeatures.CQRS.Commands.DTOs;
using User_Registration_System.Features.UserFeatures.CQRS.Orchestrators;

namespace User_Registration_System.Features.UserFeatures.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegisterDto Modle)
        {
            var result = await mediator.Send(new RegisterOrchestrator(Modle.Name, Modle.Email));
            return StatusCode(result.StatusCode, new { message = result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await mediator.Send(new CQRS.Quries.GetAllUsers.GetAllUsersQuery());
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }
            return StatusCode(result.StatusCode, new { message = result.Message, data = result.Data });
        }
    }
}