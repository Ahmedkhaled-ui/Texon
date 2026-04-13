using AutoMapper;
using System.Threading.Tasks;
using Texon.Domin.Contracts;
using Texon.Domin.Entities.Products;
using Texon.Service.Abstraction.Common;
using Texon.Service.Abstraction.IService;
using Texon.Shared;

namespace Texon.Service.Service
{
    public class categoryService(IUnitofWork unitofWork ,IMapper mapper) : IcategoryService
    {
        public async Task<bool> CreateCategory(CategoryDto category)
        {
            if (category == null)
                return false;

            var categorye = mapper.Map<Category>(category);

            await unitofWork.GetRepository<Category, int>().AddAsync(categorye);
            return await unitofWork.SaveChangesAsync() > 0;


        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync()
        {
            var result = await unitofWork.GetRepository<Category,int>().GetAllAsync();
            if (result == null)
                return [];

            return mapper.Map<IEnumerable<CategoryDto>>(result);

        }

        public async Task<CategoryDto> GetCategoryId(int categoryId)
        {
            var result = await unitofWork.GetRepository<Category, int>().GetByIdAsync(categoryId); 

            if(result == null)
                return null;

            return mapper.Map<CategoryDto>(result);


        }
    }
}
