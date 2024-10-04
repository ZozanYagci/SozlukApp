using MediatR;
using SozlukApp.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailViewModel>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public GetUserDetailQuery(Guid userId, string userName = null)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
