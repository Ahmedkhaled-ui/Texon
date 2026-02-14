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
            ApplyPagintion(productQuary.pageSize, productQuary.pageIndex);
            sort(productQuary);
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
        }
    
    
    private void sort(ProductQuary productQuary)
        {
            switch (productQuary.Sort)
            {
                case ProductSort.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSort.PriceDesc:
                    AddOrderByDes(p => p.Price);
                    break;
                case ProductSort.NameAsc:
                    AddOrderBy(p => p.NameEn);
                    break;
                case ProductSort.NameDesc:
                    AddOrderByDes(p => p.NameEn);
                    break;
                default:
                    AddOrderBy(p => p.NameEn);
                    break;
            }

        }

    }
}
