using NewsAPI.Model;

namespace NewsAPI.Services
{
    public interface IAnnouncementCollectionService : ICollectionService<Announcement>
    {
        Task<List<Announcement>> GetAnnouncementsByCategoryId(string categoryId);
    }
}
