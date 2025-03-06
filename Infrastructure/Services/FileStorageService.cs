using Application.Interfaces;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly FileStorageSettings _settings;
    public FileStorageService(IOptions<FileStorageSettings> options)
    {
        _settings = options.Value;
    }

    public async Task<string> SaveFile(IFormFile file)
    {
        if (!_settings.AllowedImageTypes.Contains(file.ContentType))
        {
            throw new InvalidOperationException("Invalid file format. Only image files are allowed.");
        }
        
        if (file.Length > _settings.MaxFileSize)
        {
            throw new InvalidOperationException("File size exceeds the maximum allowed size of 50 MB.");
        }
        
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(_settings.Path, fileName);
        
        if (!Directory.Exists(_settings.Path))
        {
            Directory.CreateDirectory(_settings.Path);
        }
        
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Combine(_settings.Path, fileName);
    }
}