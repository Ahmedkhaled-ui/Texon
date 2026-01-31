using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Texon.Domin.Contracts;

namespace Texon.Presentation.Controller.UploadFiles
{
    public class uploadFilesController(IAttachmentService attachmentService) : ApiBaseController
    {

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var path = await attachmentService.uploadPhoto(file, "uploads");
            if (string.IsNullOrEmpty(path))
                return BadRequest("File upload failed.");
            return Ok(new { FilePath = path });
        }



    }
}
