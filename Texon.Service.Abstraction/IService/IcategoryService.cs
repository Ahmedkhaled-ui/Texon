using Texon.Shared;

namespace Texon.Service.Abstraction.IService
{
    public interface IcategoryService 
    {

        public Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
        public Task<CategoryDto> GetCategoryId(int categoryId);
        public Task<bool> CreateCategory(CategoryDto category);
    }
}
