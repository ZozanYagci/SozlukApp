using MediatR;
using SozlukApp.Common.Events.EntryComment;
using SozlukApp.Common.Infrastructure;
using SozlukApp.Common;
using SozlukApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Commands.EntryComment.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
                                                exchangeType: SozlukConstants.DefaultExchangeType,
                                                queueName: SozlukConstants.CreateEntryCommentVoteQueueName,
                                                obj: new CreateEntryCommentVoteEvent()
                                                {
                                                    EntryCommentId = request.EntryCommentId,
                                                    VoteType = request.VoteType,
                                                    CreatedBy = request.CreatedBy,
                                                });

            return await Task.FromResult(true);

        }
    }
}
