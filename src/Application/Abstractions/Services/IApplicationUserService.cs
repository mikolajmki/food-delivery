using Application.Models.ApplicationModels;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface IApplicationUserService : IGenericService<ApplicationUserModel, ApplicationUser>
{
    Task<List<ApplicationUserModel>> GetAllWithTotalCounts(string userId);
    public ApplicationUserModel GetApplicationUser(int id);
}
