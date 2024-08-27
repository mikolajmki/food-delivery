using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Models;
namespace Infrastructure.Repository;

public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserService
{
    public ApplicationUser GetApplicationUser(int id)
    {
        throw new NotImplementedException();
    }
}
