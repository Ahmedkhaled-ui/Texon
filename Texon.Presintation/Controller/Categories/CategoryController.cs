using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Texon.Service.Abstraction.IService;
using Texon.Shared;

namespace Texon.Presentation.Controller.Categories
{
    public class CategoryController(IcategoryService categoryService) : ApiBaseController
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllCategory()
        {
            var result =  await categoryService.GetAllCategoryAsync();
            return Ok(result);
        }
        [HttpPost("Create")]
        [Authorize]
        public async Task<ActionResult> CreateCategory(CategoryDto category)
        {
            var result = await categoryService.CreateCategory(category);
            return Ok(result);
        }
    }
}
