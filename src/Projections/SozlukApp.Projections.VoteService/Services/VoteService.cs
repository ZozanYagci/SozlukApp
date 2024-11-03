using Dapper;
using Microsoft.Data.SqlClient;
using SozlukApp.Common.Events.Entry;
using SozlukApp.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Projections.VoteService.Services
{
    public class VoteService
    {
        private readonly string connectionString;

        public VoteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryVote (CreateEntryVoteEvent vote)
        {
            // eski oyu silip, yeni oyu insert et

            await DeleteEntryVote(vote.EntryId, vote.CreatedBy); 

            using var connection= new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO ENTRYVOTE (Id, CreateDate, EntryId, VoteType, CreatedById) VALUES (@Id, GETDATE(), @EntryId, @VoteType, @CreatedById)",
                new
                {
                    Id=Guid.NewGuid(),
                    EntryId=vote.EntryId,
                    VoteType=(int)vote.VoteType,
                    CreatedBy=vote.CreatedBy
                });

        }

        public async Task DeleteEntryVote(Guid entryId, Guid userId)
        {
            using var connection= new SqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryVote WHERE EntryId=@EntryId=@EntryId AND CREATEDBY=@UserId",
                new
                {
                    EntryId = entryId,
                    UserId = userId
                });
        }
    }
}
