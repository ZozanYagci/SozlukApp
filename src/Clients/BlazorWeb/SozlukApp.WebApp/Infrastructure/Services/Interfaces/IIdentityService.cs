using SozlukApp.Common.Models.RequestModels;

namespace SozlukApp.WebApp.Infrastructure.Services.Interfaces
{
    public interface IIdentityService
    {
        bool IsLoggedIn { get; }
        Task<bool> Login(LoginUserCommand command);
        Task LogOut();
    }
}
