using AutoMapper;
using E_Commerce.Shared;
using Texon.Domin.Contracts;
using Texon.Domin.Entities.Products;
using Texon.Service.Abstraction.Common;
using Texon.Service.Abstraction.IService;
using Texon.Service.specfications;
using Texon.Shared.ProductDto;

namespace Texon.Service.Service
{
    public class ProductService(IUnitofWork unitofWork , IMapper mapper ) : IproductService
    {
        public async Task<bool> CreateProductAsync(ProductRequest product )
        {
            try
            {
                var category = await unitofWork.GetRepository<Category, int>().GetByIdAsync(product.CategoryId);
                if (category == null)
                {
                    return false;
                }


               


                var products = mapper.Map<Product>(product);
                products.PhotoUrl = product.PhotoUrl;

                if (product.GalleryUrls != null && product.GalleryUrls.Any())
                {
                    products.Images = new List<ProductImages>();
                    foreach (var url in product.GalleryUrls)
                    {
                        products.Images.Add(new ProductImages
                        {
                            ImageUrl = url
                        });
                    }
                }
               await  unitofWork.GetRepository<Product , int>().AddAsync(products);
                return await unitofWork.SaveChangesAsync() > 0;



            }
            catch (Exception ex)
            {

                return false;
            }  
        }

       

        public async Task<bool> DeleteProductAsync(int id)
        {
            var result = await unitofWork.GetRepository<Product , int>().GetByIdAsync(id);
            if (result == null)
                return false;
            unitofWork.GetRepository<Product , int>().Remove(result);
            return await unitofWork.SaveChangesAsync() > 0;
        }

        public async Task<PagenatedResult<ProductResponse>> GetAllProductsAsync(string lang , ProductQuary productQuary)
        {
            var spec = new ProductWithCategory(productQuary);
                
                var products = await unitofWork.GetRepository<Product , int>().GetAllAsync(spec);
            if (!products.Any())
                return null;


            var totalCount = await unitofWork.GetRepository<Product , int>().CountAsync(new ProductCountSpecfication(productQuary));

            var result = mapper.Map<IEnumerable<ProductResponse>>(products , opt => opt.Items["lang"] = lang);

            return new(productQuary.pageIndex, result.Count(), totalCount, result);
        }



        public async Task<Result<ProductResponse>> GetProductByIdAsync(int id , string lang , CancellationToken cancellationToken)
        {
            var spec = new ProductWithCategory(id);
            var productId = await unitofWork.GetRepository<Product , int>().GetAsync(spec);
            if (productId is null)
                return Error.NotFound("","");

            return mapper.Map<ProductResponse>(productId, opt => opt.Items["lang"] = lang);




        }

      

        public async Task<bool> UpdateProductAsync(int id, ProductRequest productreq)
        {
            var spec = new ProductWithCategory(id);
            var result = await unitofWork.GetRepository<Product , int>().GetAsync(spec);
            if (result == null)
                return false;

            mapper.Map(productreq, result);
            result.PhotoUrl = productreq.PhotoUrl;
            if (productreq.GalleryUrls != null && productreq.GalleryUrls.Any())
            {
                result.Images = new List<ProductImages>();
                foreach (var url in productreq.GalleryUrls)
                {
                    result.Images.Add(new ProductImages
                    {
                        ImageUrl = url
                    });
                }
            }
            unitofWork.GetRepository<Product , int>().update(result);

            return await unitofWork.SaveChangesAsync() > 0;
        }

        #region Helper
       
        #endregion
    }
}
