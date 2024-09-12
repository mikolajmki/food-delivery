﻿using Application.Models.ApplicationModels;
using Domain.Models;
using System.Security.Principal;
namespace Application.Abstractions.Services;

public interface IReviewService : IGenericService<ReviewModel, Review>
{
    Task<List<ReviewModel>> GetReviewsOfUser(IIdentity identity);
    Task<List<ReviewModel>> GetReviewsOfAllUsers();
    Task<ReviewModel> GetReviewDetailsIncludeUser (int id);
    Task<ReviewModel> GetReviewDetails(int id);
    Task<bool> CreateReview(ReviewModel review, IIdentity identity);
    Task<List<ReviewModel>> GetByItemIdIncludeUser(int id);
}
