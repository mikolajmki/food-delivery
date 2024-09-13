using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Domain.Models;

namespace Application.Services;

internal class CategoryService : GenericService<CategoryModel, Category>, ICategoryService
{
    public CategoryService(IGenericRepository<Category> repository) : base(repository)
    {
    }

    public Task<CategoryModel> GetCategory()
    {
        throw new NotImplementedException();
    }
}
