<<<<<<< HEAD:Application/Application/Services/ReviewService.cs
﻿using Application.Abstractions;
using Application.Abstractions.Repositories;
=======
﻿using Application.Abstractions.Repositories;
>>>>>>> develop:src/Application/Services/ReviewService.cs
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Domain.Models;
using MapsterMapper;
<<<<<<< HEAD:Application/Application/Services/ReviewService.cs
using System.Security.Principal;
=======
>>>>>>> develop:src/Application/Services/ReviewService.cs

namespace Application.Services;

internal class ReviewService : GenericService<ReviewModel, Review>, IReviewService
{
    private readonly IReviewRepository _reviewRepository;
<<<<<<< HEAD:Application/Application/Services/ReviewService.cs
    private IIdentityService _identityService;
=======
>>>>>>> develop:src/Application/Services/ReviewService.cs
    private readonly IMapper _mapper;

    public ReviewService(
        IGenericRepository<Review> repository, 
        IReviewRepository reviewRepository, 
<<<<<<< HEAD:Application/Application/Services/ReviewService.cs
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

=======
        IMapper mapper 

    ): base(repository, mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task<bool> CreateReview(ReviewModel review, string userId)
    {
>>>>>>> develop:src/Application/Services/ReviewService.cs
        var enity = _mapper.Map<Review>(review);

        var d = DateTime.UtcNow;
        var date = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);

<<<<<<< HEAD:Application/Application/Services/ReviewService.cs
        enity.ApplicationUserId = id;
=======
        enity.ApplicationUserId = userId;
>>>>>>> develop:src/Application/Services/ReviewService.cs
        enity.CreatedDate = date;

        await _reviewRepository.Create(enity);

        return true;
    }
<<<<<<< HEAD:Application/Application/Services/ReviewService.cs
    public async Task<List<ReviewModel>> GetReviewsOfUser(IIdentity identity)
    {
        var id = _identityService.GetIdFromClaim(identity);

        var list = await _reviewRepository.GetReviewsOfUser(id);
=======
    public async Task<List<ReviewModel>> GetReviewsOfUser(string userId)
    {
        var list = await _reviewRepository.GetReviewsOfUser(userId);
>>>>>>> develop:src/Application/Services/ReviewService.cs
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
<<<<<<< HEAD:Application/Application/Services/ReviewService.cs
=======

    public async Task<List<ReviewModel>> GetByItemIdIncludeUser(int id)
    {
        var list = await _reviewRepository.GetByItemIdIncludeUser(id);
        var reviews = _mapper.Map<List<ReviewModel>>(list);

        return reviews;
    }
>>>>>>> develop:src/Application/Services/ReviewService.cs
}
