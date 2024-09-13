using Application.Models.ApplicationModels;
using Domain.Models;
namespace Application.Abstractions.Services;

public interface IReviewService : IGenericService<ReviewModel, Review>
{
    Task<List<ReviewModel>> GetReviewsOfUser(string userId);
    Task<List<ReviewModel>> GetReviewsOfAllUsers();
    Task<ReviewModel> GetReviewDetailsIncludeUser (int id);
    Task<ReviewModel> GetReviewDetails(int id);
    Task<bool> CreateReview(ReviewModel review, string userId);
    Task<List<ReviewModel>> GetByItemIdIncludeUser(int id);
}
