using Application.Abstractions.Repositories;
using Domain.Models;
using food_delivery.Repository;

namespace Infrastructure.Repository;

public class ItemRepository : GenericRepository<Item>, ItemService
{
    private readonly ApplicationDbContext _context;

    public Task<Item> GetItem()
    {
        throw new NotImplementedException();
    }

    public void GetPopulated()
    {
        throw new NotImplementedException();
    }
}
