using Texon.Domin.Entities.Products;
using Texon.Shared.ProductDto;

namespace Texon.Service.Abstraction.IService
{
    public interface IproductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync(string lang , ProductQuary productQuary);
        Task<ProductResponse> GetProductByIdAsync(int id ,string lang, CancellationToken cancellationToken);
        Task<bool> CreateProductAsync(ProductRequest product );
        Task<bool> UpdateProductAsync(int id, ProductRequest product);

        Task<bool> DeleteProductAsync(int id);
    }
}
