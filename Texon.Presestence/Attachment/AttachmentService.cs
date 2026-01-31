using Microsoft.AspNetCore.Http;
using Texon.Domin.Contracts;
using Microsoft.AspNetCore.Hosting;

namespace Texon.Persistence.Attachment
{
    public class AttachmentService : IAttachmentService
    {
        private readonly long MaxFileSize = 2 * 1024 * 1024; 
        private readonly string[] AllowedFileTypes =  { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly IWebHostEnvironment _env;

        public AttachmentService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public bool deletePhoto(string fileName, string folderName)
        {
            try
            {
                 if(string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName))
                    return false;

                 var path = Path.Combine(_env.WebRootPath, "image",folderName, fileName);

                if (File.Exists(path))
                {
                    File.Delete(path);
                        return true;
                }

                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

public async Task<string> uploadPhoto(IFormFile file, string folderName)
{
    try
    {
        if (file is null || file.Length == 0) return null;
        if (file.Length > MaxFileSize) return null;

        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!AllowedFileTypes.Contains(fileExtension)) return null;

        var rootPath = _env.WebRootPath;
        if (string.IsNullOrEmpty(rootPath))
        {
            rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        var uploadsFolder = Path.Combine(rootPath, "image", folderName);

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var fileName = Guid.NewGuid().ToString() + fileExtension;
        var filePath = Path.Combine(uploadsFolder, fileName);

        // --- استخدام Async للحفظ لمنع الـ Deadlock ---
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream); // لازم await و CopyToAsync
        }

        return fileName;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"FATAL ERROR in uploadPhoto: {ex.Message}");
        return null;
    }
}    }
}
