using Application.Abstractions.Repositories;
using Domain.Models;
namespace Infrastructure.Repository;

public class ApplicationUserRepository : GenericRepository<ApplicationUser>, ApplicationUserService
{
    public ApplicationUser GetApplicationUser(int id)
    {
        throw new NotImplementedException();
    }
}
