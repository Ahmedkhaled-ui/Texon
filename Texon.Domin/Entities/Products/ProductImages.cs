using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texon.Domin.Entities.Products
{
    public class ProductImages : BaseEntity<int>
    {
        public string ImageUrl { get; set; }

        // العلاقة مع المنتج
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
