using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Texon.Service.Abstraction.IService;

namespace Texon.Presentation.Controller
{
    public class CategoryController(IcategoryService categoryService) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetAllCategory()
        {
            var result =  await categoryService.GetAllCategoryAsync();
            return Ok(result);
        }
    }
}
