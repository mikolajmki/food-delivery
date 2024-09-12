using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Domain.Models;
using MapsterMapper;
using System.Security.Principal;

namespace Application.Services;

internal class ReviewService : GenericService<ReviewModel, Review>, IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private IIdentityService _identityService;
    private readonly IMapper _mapper;

    public ReviewService(
        IGenericRepository<Review> repository, 
        IReviewRepository reviewRepository, 
        IMapper mapper, 
        IIdentityService identityService

    ): base(repository)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<bool> CreateReview(ReviewModel review, IIdentity identity)
    {
        var id = _identityService.GetIdFromClaim(identity);

        var enity = _mapper.Map<Review>(review);

        var d = DateTime.UtcNow;
        var date = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);

        enity.ApplicationUserId = id;
        enity.CreatedDate = date;

        await _reviewRepository.Create(enity);

        return true;
    }
    public async Task<List<ReviewModel>> GetReviewsOfUser(IIdentity identity)
    {
        var id = _identityService.GetIdFromClaim(identity);

        var list = await _reviewRepository.GetReviewsOfUser(id);
        var reviews = _mapper.Map<List<ReviewModel>>(list);

        return reviews;
    }

    public async Task<List<ReviewModel>> GetReviewsOfAllUsers()
    {
        var list = await _reviewRepository.GetReviewsOfAllUsers();
        var reviews = _mapper.Map<List<ReviewModel>>(list);

        return reviews;
    }

    public async Task<ReviewModel> GetReviewDetailsIncludeUser(int id)
    {
        var entity = await _reviewRepository.GetReviewDetailsIncludeUser(id);
        var review = _mapper.Map<ReviewModel>(entity);

        return review;
    }

    public async Task<ReviewModel> GetReviewDetails(int id)
    {
        var entity = await _reviewRepository.GetReviewDetails(id);
        var review = _mapper.Map<ReviewModel>(entity);

        return review;
    }

    public async Task<List<ReviewModel>> GetByItemIdIncludeUser(int id)
    {
        var list = await _reviewRepository.GetByItemIdIncludeUser(id);
        var reviews = _mapper.Map<List<ReviewModel>>(list);

        return reviews;
    }
}
