﻿using MediatR;
using SozlukApp.Common.Models.Page;
using SozlukApp.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQuery:BasePagedQuery, IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public Guid? UserId { get; set; }
        public string UserName { get; set; }

        public GetUserEntriesQuery(Guid? userId, string userName=null, int page=1, int pageSize=10):base(page, pageSize)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
