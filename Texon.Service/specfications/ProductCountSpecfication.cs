using System.Linq.Expressions;
using Texon.Shared.ProductDto;

namespace Texon.Service.specfications
{
    public sealed class ProductCountSpecfication(ProductQuary productQuary) :
        BaseSpecfications<Product>(createcriteria(productQuary))
    {

        private static Expression<Func<Product, bool>> createcriteria(ProductQuary productQuary)
        {
            return x => (!productQuary.categoryId.HasValue || x.CategoryId == productQuary.categoryId) &&
            (string.IsNullOrWhiteSpace(productQuary.Search) || x.NameAr.Contains(productQuary.Search) || x.NameEn.Contains(productQuary.Search));
        }
    }
}
