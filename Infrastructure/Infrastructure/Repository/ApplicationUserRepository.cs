using Application.Abstractions.Repositories;
using Domain.Models;
using food_delivery.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ApplicationUser>> GetAllWithTotalCounts(string userId)
    {
        var users = await _context.ApplicationUsers
            .Where(x => x.Id != int.Parse(userId))
            .ToListAsync();

        return users;
    }

    public ApplicationUser GetApplicationUser(int id)
    {
        throw new NotImplementedException();
    }
}
