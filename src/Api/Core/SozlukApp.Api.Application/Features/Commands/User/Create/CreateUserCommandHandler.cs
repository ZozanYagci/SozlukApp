using AutoMapper;
using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Common;
using SozlukApp.Common.Events.User;
using SozlukApp.Common.Infrastructure;
using SozlukApp.Common.Infrastructure.Exceptions;
using SozlukApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await _userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            if (existsUser is not null)
                throw new DatabaseValidationException("User already exists!");

            var dbUser = _mapper.Map<Domain.Models.User>(request);

            var rows=await _userRepository.AddAsync(dbUser);

            //Email Changed/Created

            if (rows > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.EmailAddress
                };

                QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                                                   exchangeType:SozlukConstants.DefaultExchangeType,
                                                   queueName:SozlukConstants.UserEmailChangeQueueName,
                                                   obj: @event);
            }
            return dbUser.Id;
        }
    }
}
