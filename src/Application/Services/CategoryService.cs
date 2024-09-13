using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Domain.Models;
using MapsterMapper;

namespace Application.Services;

internal class CategoryService : GenericService<CategoryModel, Category>, ICategoryService
{
    private readonly IMapper mapper;

    public CategoryService(IGenericRepository<Category> repository, IMapper mapper) : base(repository, mapper)
    {
        this.mapper = mapper;
    }

    public Task<CategoryModel> GetCategory()
    {
        throw new NotImplementedException();
    }
}
