using Domain.Models;
namespace Application.Abstractions.Repositories;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<List<Review>> GetReviewsOfAllUsers();
    Task<List<Review>> GetReviewsOfItemsByUser(List<int> ids, string userId);
    Task<List<Review>> GetReviewsOfUser(string id);
    Task<Review> GetReviewDetailsIncludeUser(int id);
    Task<Review> GetReviewDetails(int id);
    Task<List<Review>> GetReviewsOfItem(int itemId);
    Task<List<Review>> GetByItemIdIncludeUser(int id);
}
