namespace Infrastructure.Settings;

public class FileStorageSettings
{
    public string Path { get; set; } = string.Empty;
    public long MaxFileSize { get; set; }
    public string[] AllowedImageTypes { get; set; }
}