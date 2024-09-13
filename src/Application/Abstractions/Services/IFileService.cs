namespace Application.Abstractions.Services;

public interface IFileService
{
    Task<bool> SaveImage(Stream image);
}
