using SozlukApp.WebApp.Infrastructure.Services.Interfaces;

namespace SozlukApp.WebApp.Infrastructure.Services
{
    public class FavService:IFavService
    {
        private readonly HttpClient _client;

        public FavService(HttpClient client)
        {
            _client = client;
        }

        public async Task CreatedEntryCommentFav(Guid? entryCommentId)
        {
            await _client.PostAsync($"/api/favorite/EntryComment/{entryCommentId}", null);
        }

        public async Task CreatedEntryFav(Guid? entryId)
        {
            await _client.PostAsync($"/api/favorite/Entry/{entryId}", null);
        }

        public async Task DeleteEntryCommentFav(Guid? entryCommentId)
        {
            await _client.PostAsync($"/api/favorite/DeleteEntryCommentFav/{entryCommentId}", null);
        }

        public async Task DeleteEntryFav(Guid? entryId)
        {
            await _client.PostAsync($"/api/favorite/DeleteEntryFav/{entryId}", null);
        }
    }
}
