using Application.Abstractions.Services;

namespace Infrastructure.Services;

internal class FileService : IFileService
{

    public async Task<bool> SaveImage(Stream image)
    {
        var uploadDir = @"Images/Items";
        var filename = "IMG-" + Guid.NewGuid().ToString();
        var path = Path.Combine(Directory.GetCurrentDirectory(), uploadDir, filename);
        await image.CopyToAsync(new FileStream(path, FileMode.Create));

        return true;
    }
}
