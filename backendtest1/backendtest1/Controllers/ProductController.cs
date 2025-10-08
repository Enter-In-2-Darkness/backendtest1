using Microsoft.AspNetCore.Mvc;
using backendtest1.Models;


namespace backendtest1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private static List<ProductModel> _productModel = new();
        private static int _numberId = 1;

        [HttpGet]
        public ActionResult<ApiResponse<IEnumerable<ProductModel>>> GetProducts()
        {
            return Ok(new ApiResponse<IEnumerable<ProductModel>>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = _productModel.OrderBy(p => p.Id).ToList()
            });
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ProductModel>> GetProductById(int id)
        {
            var product = _productModel.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(new ApiResponse<ProductModel>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse<ProductModel>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = product
            });
        }

        [HttpPost]
        public ActionResult<ApiResponse<ProductModel>> CreateProduct([FromBody] ProductValidateModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ApiResponse<ProductModel>
                {
                    Success = false,
                    Message = string.Join("; ", errors),
                    Data = null
                });
            }
            var newProduct = new ProductModel
            {
                Id = _numberId++,
                NumberCode = model.NumberCode,
                CreateAt = DateTime.UtcNow
            };
            _productModel.Add(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, new ApiResponse<ProductModel>
            {
                Success = true,
                Message = "Product created successfully",
                Data = newProduct
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<object>> DeleteProduct(int id)
        {
            var product = _productModel.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = null
                });
            }
            _productModel.Remove(product);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Product deleted successfully",
                Data = null
            });
        }

    }
}
