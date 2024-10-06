using SozlukApp.Common.Events.User;
using SozlukApp.Common.Infrastructure.Exceptions;
using SozlukApp.Common.Infrastructure.Results;
using SozlukApp.Common.Models.Queries;
using SozlukApp.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace SozlukApp.WebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {

        private readonly HttpClient _client;

        public UserService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var command = new ChangeUserPasswordCommand(null, oldPassword, newPassword);
            var httpResponse = await _client.PostAsJsonAsync($"/api/User/ChangePassword", command);

            if(httpResponse !=null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var responseStr=await httpResponse.Content.ReadAsStringAsync();
                    var validation=JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }
                return false;
            }
            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
        {
            var userDetail = await _client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");
            return userDetail;
        }

        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var userDetail=await _client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/username/{userName}");
            return userDetail;
        }

        public async Task<bool> UpdateUser(UserDetailViewModel user)
        {
            var res = await _client.PostAsJsonAsync($"/api/user/update", user);
            return res.IsSuccessStatusCode;
        }
    }
}
