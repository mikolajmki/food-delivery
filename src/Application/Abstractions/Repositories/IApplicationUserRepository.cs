using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
{
    Task<List<ApplicationUser>> GetAllWithTotalCounts(string userId);
    public ApplicationUser GetApplicationUser(int id);
}
