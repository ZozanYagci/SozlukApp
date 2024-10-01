using MediatR;
using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Common.Events.User;
using SozlukApp.Common.Infrastructure;
using SozlukApp.Common.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Commands.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler:IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if(!request.UserId.HasValue)
                throw new ArgumentNullException(nameof(request.UserId));

            var dbUser=await _userRepository.GetByIdAsync(request.UserId.Value);

            if (dbUser is null)
                throw new DatabaseValidationException("User not found!");

            var encPass = PasswordEncryptor.Encrpt(request.OldPassword);
            if (dbUser.Password != encPass)
                throw new DatabaseValidationException("Old password wrong!");

            dbUser.Password = encPass;

            await _userRepository.UpdateAsync(dbUser);

            return true;
        }
    }

}
