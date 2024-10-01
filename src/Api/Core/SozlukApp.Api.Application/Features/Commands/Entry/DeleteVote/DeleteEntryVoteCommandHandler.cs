using MediatR;
using SozlukApp.Common.Events.Entry;
using SozlukApp.Common.Infrastructure;
using SozlukApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
                 exchangeType: SozlukConstants.DefaultExchangeType,
                 queueName: SozlukConstants.DeleteEntryVoteQueueName,
                 obj: new DeleteEntryVoteEvent()
                 {
                     EntryId = request.EntryId,
                     CreatedBy = request.UserId
                 });

            return await Task.FromResult(true);

        }
    }
}
