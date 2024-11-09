using Dapper;
using Microsoft.Data.SqlClient;
using SozlukApp.Common.Events.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukApp.Projections.UserService.Services
{
    public class UserService
    {
        private string connStr;

        public UserService(IConfiguration configuration)
        {

            connStr = configuration.GetConnectionString("SqlServer");
        }

        public async Task<Guid> CreateEmailConfirmation(UserEmailChangedEvent @event)
        {
            var guid=Guid.NewGuid();

            using var connection = new SqlConnection(connStr);

            await connection.ExecuteAsync("INSERT INTO EmailConfirmation (Id, CreateDate, OldEmailAddress, NewEmailAddress) VALUES (@Id, GETDATE(), @OldEmailAddress, @NewEmailAddress)",
                new
                {
                    Id=guid,
                    OldEmailAddress=@event.OldEmailAddress,
                    NewEmailAddress=@event.NewEmailAddress,
                });

            return guid;
        }
    }
}
