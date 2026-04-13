using Microsoft.AspNetCore.Http;
using Texon.Domin.Contracts;
using Microsoft.AspNetCore.Hosting;

namespace Texon.Persistence.Attachment
{
    public class AttachmentService : IAttachmentService
    {
        private readonly long MaxFileSize = 5 * 1024 * 1024; 
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
                // 1. التحقق الأساسي
                if (file is null || file.Length == 0) return null;
                if (file.Length > MaxFileSize) return null;

                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (!AllowedFileTypes.Contains(fileExtension)) return null;

                // 2. تأمين الـ Root Path (أهم خطوة)
                // بنستخدم Directory.GetCurrentDirectory() كبديل آمن لو الـ Env مش قاري الـ wwwroot
                var rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                // التأكد من وجود الـ wwwroot نفسه الأول
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                // 3. بناء المسار خطوة بخطوة لضمان عدم وجود Null
                var imagePath = Path.Combine(rootPath, "image");
                if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);

                var uploadsFolder = Path.Combine(imagePath, folderName);
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                // 4. إنشاء اسم الملف وحفظه
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                {
                    await file.CopyToAsync(fileStream);
                }

                // رجع المسار النسبي عشان تحفظه في الداتابيز (أسهل في العرض)
                return Path.Combine("image", folderName, fileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                // اطبع الـ StackTrace كامل عشان نعرف السطر اللي بيضرب فين بالظبط
                Console.WriteLine($"Critical Error in uploadPhoto: {ex.ToString()}");
                return null;
            }
        }
    }
}
