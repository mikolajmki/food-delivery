using Application.Models.ApplicationModels;
using Domain.Models;
using System.Security.Principal;

namespace Application.Abstractions.Services;

public interface IApplicationUserService : IGenericService<ApplicationUserModel, ApplicationUser>
{
    Task<List<ApplicationUserModel>> GetAllWithTotalCounts(IIdentity identity);
    public ApplicationUserModel GetApplicationUser(int id);
}
