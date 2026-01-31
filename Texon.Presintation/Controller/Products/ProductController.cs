using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Texon.Service.Abstraction.IService;
using Texon.Shared.ProductDto;

namespace Texon.Presentation.Controller.Products
{
    public class ProductController(IproductService productService) : ApiBaseController
    {

        [HttpPost("createProduct")]

        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest product)

        {
            var result = await productService.CreateProductAsync(product);

            return Ok(result);
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQuary productQuary)
        {
            var products = await productService.GetAllProductsAsync("ar", productQuary);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken)
        {
            var product = await productService.GetProductByIdAsync(id, "en", cancellationToken);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPut("update")]
        public async Task<ActionResult> updateProduct(int id, ProductRequest productRequest)
        {
            var result = await productService.UpdateProductAsync(id, productRequest);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await productService.DeleteProductAsync(id);
            return Ok(result);
        }
    }
}
