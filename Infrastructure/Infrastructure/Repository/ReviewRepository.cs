using Application.Abstractions;
using Domain.Models;

namespace Infrastructure.Repository;

internal class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public Task<Review> GetReview()
    {
        throw new NotImplementedException();
    }
}
