using Domain.Models;

namespace Application.Abstractions.Services;

public interface IApplicationUserService : IGenericService<ApplicationUser>
{
    public ApplicationUser GetApplicationUser(int id);
}
