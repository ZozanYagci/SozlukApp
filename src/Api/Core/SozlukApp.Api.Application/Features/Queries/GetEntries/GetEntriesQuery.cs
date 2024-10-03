using MediatR;
using SozlukApp.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Api.Application.Features.Queries.GetEntries
{
    public class GetEntriesQuery:IRequest<List<GetEntriesViewModel>>
    {
        public bool TodayEntries { get; set; }
        public int Count { get; set; } = 100;
    }
}
