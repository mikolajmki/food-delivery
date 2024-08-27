using Application.Models;
namespace Application.Abstractions.Services;

public interface IReviewService : IGenericService<ReviewModel>
{
    Task<ReviewModel> GetReview();
}
