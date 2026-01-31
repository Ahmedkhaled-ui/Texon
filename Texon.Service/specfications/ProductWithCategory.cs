using System.Linq.Expressions;
using Texon.Shared.ProductDto;

namespace Texon.Service.specfications
{
    public class ProductWithCategory : BaseSpecfications<Product>
    {
        public ProductWithCategory(ProductQuary productQuary)
            : base(createcriteria(productQuary))
        {
            AddInclude(p => p.Category);
        }

        public ProductWithCategory(int id)
           : base(x => x.Id == id)
        {
            AddInclude(p => p.Category);
            AddInclude(p=> p.Images);
        }

        private static Expression<Func<Product, bool>> createcriteria(ProductQuary productQuary)
        {
            return productQuary.categoryId.HasValue
                ? p => p.CategoryId == productQuary.categoryId.Value
                : p => true;
        } }
}
