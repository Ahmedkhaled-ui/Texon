using E_Commerce.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Texon.Domin.Entities.Products;
using Texon.Service.Abstraction.Common;
using Texon.Shared.ProductDto;

namespace Texon.Service.Abstraction.IService
{
    public interface IproductService
    {
        Task<PagenatedResult<ProductResponse>> GetAllProductsAsync(string lang , ProductQuary productQuary);
        Task<Result<ProductResponse>> GetProductByIdAsync(int id ,string lang, CancellationToken cancellationToken);
        Task<bool> CreateProductAsync(ProductRequest product );
        Task<bool> UpdateProductAsync(int id, ProductRequest product);

        Task<bool> DeleteProductAsync(int id);
    }
}
