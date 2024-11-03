using Dapper;
using Microsoft.Data.SqlClient;
using SozlukApp.Common.Events.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Projections.FavoriteService.Services
{
    public class FavoriteService
    {
        private readonly string connectionString;

        public FavoriteService(string connectionString)
        {
            this.connectionString = connectionString;
        
        }

        public async Task CreateEntryFav(CreateEntryFavEvent @event)
        {

            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EntryFavorite (Id, EntryId, FavoritedUserId, CreateDate) VALUES(@Id, @EntryId, @CreatedBy, GETDATE())",
                new
                {
                    Id=Guid.NewGuid(),
                    EntryId=@event.EntryId,
                    CreatedId=@event.CreatedBy
                });
        }
    }
}
