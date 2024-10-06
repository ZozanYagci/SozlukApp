namespace SozlukApp.WebApp.Infrastructure.Services.Interfaces
{
    public interface IFavService
    {
        Task CreatedEntryCommentFav(Guid? entryCommentId);
        Task CreatedEntryFav(Guid? entryId);
        Task DeleteEntryCommentFav(Guid? entryCommentId);
        Task DeleteEntryFav(Guid? entryId);
    }
}
