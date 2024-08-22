using Application.Abstractions.Repositories;
using Domain.Models;
namespace Application.Abstractions;

public interface IReviewService : IGenericService<Review>
{
    Task<Review> GetReview();
}
