using Application.Models.ApplicationModels;
using System.Security.Principal;

namespace Application.Abstractions.Services;

public interface IApplicationUserService : IGenericService<ApplicationUserModel>
{
    Task<List<ApplicationUserModel>> GetAllWithTotalCounts(IIdentity identity);
    public ApplicationUserModel GetApplicationUser(int id);
}
