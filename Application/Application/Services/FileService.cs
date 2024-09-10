using Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

internal class FileService : IFileService
{

    public async Task<bool> SaveImage(IFormFile image)
    {
        var uploadDir = @"Images/Items";
        var filename = Guid.NewGuid().ToString() + "-" + image.FileName;
        var path = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir, filename);
        await image.CopyToAsync(new FileStream(path, FileMode.Create));
    }
}
