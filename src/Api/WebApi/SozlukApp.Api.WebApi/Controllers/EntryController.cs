﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SozlukApp.Api.Application.Features.Queries.GetEntries;
using SozlukApp.Api.Application.Features.Queries.GetEntryComments;
using SozlukApp.Api.Application.Features.Queries.GetEntryDetail;
using SozlukApp.Api.Application.Features.Queries.GetMainPageEntries;
using SozlukApp.Api.Application.Features.Queries.GetUserEntries;
using SozlukApp.Common.Models.Queries;
using SozlukApp.Common.Models.RequestModels;

namespace SozlukApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class EntryController : BaseController
    {
        private readonly IMediator _mediator;

        public EntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery]GetEntriesQuery query)
        {
            var entries = await _mediator.Send(query);
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result=await _mediator.Send(new GetEntryDetailQuery(id, UserId));

            return Ok(result);
        }

        [HttpGet]
        [Route("Comments/{id}")]
        public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
        {
            var result = await _mediator.Send(new GetEntryCommentsQuery(id, UserId, page, pageSize));

                return Ok(result);
        }

        [HttpGet]
        [Route("UserEntries")]
        [Authorize]
        public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
        {
            if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
                userId = UserId.Value;

            var result = await _mediator.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));

            return Ok(result);
        }
        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
        {
            var entries = await _mediator.Send(new GetMainPageEntriesQuery(UserId, page, pageSize));
            return Ok(entries);
        }

        [HttpPost]
        [Route("CreateEntry")]
        [Authorize] 
        public async Task<IActionResult> CreateEntry(CreateEntryCommand command)
        {
            if (!command.CreateById.HasValue)
                command.CreateById = UserId;
            var result=await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntryComment")]
        [Authorize]
        public async Task<IActionResult> CreateEntryComment(CreateEntryCommentCommand command)
        {
            if (!command.CreateById.HasValue)
                command.CreateById = UserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route("Search")]

        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
