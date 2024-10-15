﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SozlukApp.Api.Application.Features.Commands.User.ConfirmEmail;
using SozlukApp.Api.Application.Features.Queries.GetUserDetail;
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

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _mediator.Send(new GetUserDetailQuery(id));

            return Ok(user);
        }

        [HttpGet]
        [Route("UserName/{userName}")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));

            return Ok(user);
        }

        [HttpPost]
        [Route("Login")]
       
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var res = await _mediator.Send(command);

            return Ok(res);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordCommand command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;
            var guid = await _mediator.Send(command);
            return Ok(guid);
        }

    }
}
