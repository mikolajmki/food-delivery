using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
{
    public ApplicationUser GetApplicationUser(int id);
}
