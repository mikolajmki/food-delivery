using Application.Abstractions.Repositories;
using Application.Models.ApplicationModels;
using Domain.Models;
using System.Security.Principal;
namespace Application.Abstractions;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<List<Review>> GetReviewsOfAllUsers();
    Task<List<Review>> GetReviewsOfItemsByUser(List<int> ids, string userId);
    Task<List<Review>> GetReviewsOfUser(string id);
    Task<Review> GetReviewDetailsIncludeUser(int id);
    Task<Review> GetReviewDetails(int id);
}
