using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Domain.Models;
using MapsterMapper;
using System.Security.Principal;

namespace Application.Services;

internal class ApplicationUserService : GenericService<ApplicationUserModel, ApplicationUser>, IApplicationUserService
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IMapper _mapper;

    public ApplicationUserService(
        IGenericRepository<ApplicationUser> repository, 
        IMapper mapper, 
        IApplicationUserRepository applicationUserRepository

    ) : base(repository)
    {
        _mapper = mapper;
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task<List<ApplicationUserModel>> GetAllWithTotalCounts(string userId)
    {
        var list = await _applicationUserRepository.GetAllWithTotalCounts(userId);
        var users = _mapper.Map<List<ApplicationUserModel>>(list);

        return users;
    }

    public ApplicationUserModel GetApplicationUser(int id)
    {
        throw new NotImplementedException();
    }
}
