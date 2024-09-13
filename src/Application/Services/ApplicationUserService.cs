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
<<<<<<< HEAD:Application/Application/Services/ApplicationUserService.cs
    private readonly IIdentityService _identityService;
=======
>>>>>>> develop:src/Application/Services/ApplicationUserService.cs
    private readonly IMapper _mapper;

    public ApplicationUserService(
        IGenericRepository<ApplicationUser> repository, 
        IMapper mapper, 
        IApplicationUserRepository applicationUserRepository

<<<<<<< HEAD:Application/Application/Services/ApplicationUserService.cs
    ) : base(repository)
=======
    ) : base(repository, mapper)
>>>>>>> develop:src/Application/Services/ApplicationUserService.cs
    {
        _mapper = mapper;
        _applicationUserRepository = applicationUserRepository;
    }

<<<<<<< HEAD:Application/Application/Services/ApplicationUserService.cs
    public async Task<List<ApplicationUserModel>> GetAllWithTotalCounts(IIdentity identity)
    {
        var id = _identityService.GetIdFromClaim(identity);

        var list = await _applicationUserRepository.GetAllWithTotalCounts(id);
=======
    public async Task<List<ApplicationUserModel>> GetAllWithTotalCounts(string userId)
    {
        var list = await _applicationUserRepository.GetAllWithTotalCounts(userId);
>>>>>>> develop:src/Application/Services/ApplicationUserService.cs
        var users = _mapper.Map<List<ApplicationUserModel>>(list);

        return users;
    }

    public ApplicationUserModel GetApplicationUser(int id)
    {
        throw new NotImplementedException();
    }
}
