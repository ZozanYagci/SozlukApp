using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SozlukApp.Api.Application.Features.Queries.GetEntries;
using SozlukApp.Api.Application.Features.Queries.GetMainPageEntries;
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

        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
        {
            var entries = await _mediator.Send(new GetMainPageEntriesQuery(UserId, page, pageSize));
            return Ok(entries);
        }

        [HttpPost]
        [Route("CreateEntry")]
        public async Task<IActionResult> CreateEntry(CreateEntryCommand command)
        {
            if (!command.CreateById.HasValue)
                command.CreateById = UserId;
            var result=await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntryComment")]
        public async Task<IActionResult> CreateEntryComment(CreateEntryCommentCommand command)
        {
            if (!command.CreateById.HasValue)
                command.CreateById = UserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
