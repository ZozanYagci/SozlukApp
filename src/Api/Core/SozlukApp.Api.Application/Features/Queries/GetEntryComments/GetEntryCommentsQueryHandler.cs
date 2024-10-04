using MediatR;
using Microsoft.EntityFrameworkCore;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Common.Infrastructure.Extensions;
using SozlukApp.Common.Models.Page;
using SozlukApp.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
    {
        private readonly IEntryCommentRepository _entryCommentRepository;

        public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
        {
            _entryCommentRepository = entryCommentRepository;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
        {
          var query=_entryCommentRepository.AsQueryable();

            query = query.Include(i => i.EntryCommentFavorites)
                .Include(i => i.CreatedBy)
                .Include(i => i.EntryCommentVotes)
                .Where(i => i.EntryId == request.EntryId);

            var list = query.Select(i => new GetEntryCommentsViewModel()
            {
                Id=i.Id,
                Content=i.Content,
                IsFavorited=request.UserId.HasValue && i.EntryCommentFavorites.Any(j=>j.CreatedById==request.UserId),
                FavoritedCount=i.EntryCommentFavorites.Count,
                CreatedDate=i.CreateDate,
                CreatedByUserName=i.CreatedBy.UserName,
                VoteType=
                request.UserId.HasValue && i.EntryCommentVotes.Any(j=>j.CreatedById==request.UserId)
                ? i.EntryCommentVotes.FirstOrDefault(j=>j.CreatedById==request.UserId).VoteType
                :Common.ViewModels.VoteType.None
            });
            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }
    }
}
