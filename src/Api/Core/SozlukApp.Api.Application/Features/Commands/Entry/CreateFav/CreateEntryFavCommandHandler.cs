using MediatR;
using SozlukApp.Common;
using SozlukApp.Common.Events.Entry;
using SozlukApp.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {
        public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
                exchangeType: SozlukConstants.DefaultExchangeType,
                queueName: SozlukConstants.CreateEntryFavQueueName,
                obj: new CreateEntryFavEvent()
                {
                    EntryId=request.EntryId.Value,
                    CreatedBy=request.UserId.Value
                });

            return Task.FromResult(true);
        }
    }
}
