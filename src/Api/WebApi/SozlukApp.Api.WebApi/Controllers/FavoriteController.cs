using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SozlukApp.Api.Application.Features.Commands.Entry.CreateFav;
using SozlukApp.Api.Application.Features.Commands.Entry.DeleteFav;
using SozlukApp.Api.Application.Features.Commands.EntryComment.CreateFav;
using SozlukApp.Api.Application.Features.Commands.EntryComment.DeleteFav;

namespace SozlukApp.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : BaseController
    {
        private readonly IMediator mediator;

        public FavoriteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("Entry/{entryId}")]

        public async Task<IActionResult> CreateEntryFav(Guid entryId)
        {
            var result=await mediator.Send(new CreateEntryFavCommand(entryId, UserId));

            return Ok(result);

        }


        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]

        public async Task<IActionResult> CreateEntryCommentFav(Guid entryCommentId)
        {
            var result = await mediator.Send(new CreateEntryCommentFavCommand(entryCommentId, UserId.Value));

            return Ok(result);

        }


        [HttpPost]
        [Route("DeleteEntryFav/{entryId}")]

        public async Task<IActionResult> DeleteEntryFav(Guid entryId)
        {
            var result = await mediator.Send(new DeleteEntryFavCommand(entryId, UserId.Value));

            return Ok(result);

        }


        [HttpPost]
        [Route("DeleteEntryCommentFav/{entryCommentId}")]

        public async Task<IActionResult> DeleteEntryCommentFav(Guid entryCommentId)
        {
            var result = await mediator.Send(new DeleteEntryCommentFavCommand(entryCommentId, UserId.Value));

            return Ok(result);

        }
    }
}
