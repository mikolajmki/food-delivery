using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Services;

public interface IFileService
{
    Task<bool> SaveImage(IFormFile image);
}
