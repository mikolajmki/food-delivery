using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models;
using Domain.Models;
using MapsterMapper;

namespace Application.Services;

internal class SubcategoryService : GenericService<SubcategoryModel, Subcategory>, ISubcategoryService
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly IMapper _mapper;

    public SubcategoryService(
        IGenericRepository<Subcategory> repository, 
        ISubcategoryRepository subcategoryRepository
    ) : base(repository)
    {
        _subcategoryRepository = subcategoryRepository;
    }

    public async Task<List<SubcategoryModel>> GetSubcategoriesOfCategoryId(int id)
    {
        var list = await _subcategoryRepository.GetSubcategoryOfCategoryId(id);
        var subcategories = _mapper.Map<List<SubcategoryModel>>(list);

        return subcategories;
    }
}
