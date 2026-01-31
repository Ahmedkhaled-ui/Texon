using Microsoft.AspNetCore.Http;

namespace Texon.Domin.Contracts
{
    public interface IAttachmentService
    {
        Task<string> uploadPhoto(IFormFile File , string folderName);
        bool deletePhoto(string fileName , string folderName);
    }
}
