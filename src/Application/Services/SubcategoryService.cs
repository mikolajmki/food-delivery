using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
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

    public async Task<List<SubcategoryModel>> GetAllIncludeCategory()
    {
        var list = await _subcategoryRepository.GetAllIncludeCategory();
        var subcategories = _mapper.Map<List<SubcategoryModel>>(list);

        return subcategories;
    }

    public async Task<SubcategoryModel> GetFirstSubcategoryOfCategoryId(int id)
    {
        var entity = await _subcategoryRepository.GetFirstSubcategoryOfCategoryId(id);
        var subcategory = _mapper.Map<SubcategoryModel>(entity);

        return subcategory;
    }

    public async Task<List<SubcategoryModel>> GetSubcategoriesOfCategoryId(int id)
    {
        var list = await _subcategoryRepository.GetSubcategoriesOfCategoryId(id);
        var subcategories = _mapper.Map<List<SubcategoryModel>>(list);

        return subcategories;
    }
}
