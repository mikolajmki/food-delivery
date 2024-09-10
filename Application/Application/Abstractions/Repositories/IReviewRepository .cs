using Application.Abstractions.Repositories;
using Domain.Models;
namespace Application.Abstractions;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<Review> GetReviewsOfItemsByUser(int[] ids, int userId);
}
