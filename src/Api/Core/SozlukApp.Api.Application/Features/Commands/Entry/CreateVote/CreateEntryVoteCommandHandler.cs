using MediatR;
using SozlukApp.Common;
using SozlukApp.Common.Events.Entry;
using SozlukApp.Common.Infrastructure;
using SozlukApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Commands.Entry.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {

        public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
                exchangeType: SozlukConstants.DefaultExchangeType,
                queueName: SozlukConstants.CreateEntryVoteQueueName,
                obj: new CreateEntryVoteEvent()
                {
                    EntryId = request.EntryId,
                    CreatedBy = request.CreatedBy,
                    VoteType = request.VoteType,
                });

            return await Task.FromResult(true);
        }
    }
}
