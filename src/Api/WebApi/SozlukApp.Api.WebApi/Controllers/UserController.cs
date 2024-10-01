using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SozlukApp.Api.Application.Features.Commands.User.ConfirmEmail;
using SozlukApp.Common.Events.User;
using SozlukApp.Common.Models.RequestModels;

namespace SozlukApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var res = await _mediator.Send(command);

            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }


        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> ConfirmEmail(Guid id)
        {
            var guid = await _mediator.Send(new ConfirmEmailCommand() { ConfirmationId=id});;
            return Ok(guid);
        }


        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordCommand command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

    }
}
