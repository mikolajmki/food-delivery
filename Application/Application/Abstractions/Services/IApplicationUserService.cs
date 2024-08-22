using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IApplicationUserService : IGenericService<ApplicationUser>
{
    public ApplicationUser GetApplicationUser(int id);
}
