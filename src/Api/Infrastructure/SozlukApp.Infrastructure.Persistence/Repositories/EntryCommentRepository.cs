using SozlukApp.Api.Application.Interfaces.Repositories;
using SozlukApp.Api.Domain.Models;
using SozlukApp.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Infrastructure.Persistence.Repositories
{
    public class EntryCommentRepository:GenericRepository<EntryComment>, IEntryCommentRepository
    {
        public EntryCommentRepository(SozlukAppContext context) : base(context)
        {
        }
    }
}
