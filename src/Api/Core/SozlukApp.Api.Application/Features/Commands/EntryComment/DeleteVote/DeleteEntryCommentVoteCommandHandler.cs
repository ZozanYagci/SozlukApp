﻿using MediatR;
using SozlukApp.Common.Events.EntryComment;
using SozlukApp.Common.Infrastructure;
using SozlukApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Commands.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
                                                 exchangeType: SozlukConstants.DefaultExchangeType,
                                                 queueName: SozlukConstants.DeleteEntryCommentVoteQueueName,
                                                 obj: new DeleteEntryCommentVoteEvent()
                                                 {
                                                     EntryCommentId = request.EntryCommentId,

                                                     CreatedBy = request.UserId,
                                                 });

            return await Task.FromResult(true);

        }
    }
}
