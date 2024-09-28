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
    public class EntryRepository: GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(SozlukAppContext context) : base(context)
        {
        }
    }
}
