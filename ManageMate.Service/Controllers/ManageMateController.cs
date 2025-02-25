using ManageMate.DAL.Models;
using ManageMate.Service.Operations.OperationsInterface;
using ManageMate.Service.Validations;
using Microsoft.AspNetCore.Mvc;

namespace ManageMate.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageMateController : ControllerBase
    {
        private readonly IProductOperations _productOperations;
        public ManageMateController(IProductOperations productOperations)
        {
            _productOperations = productOperations;
        }

        [HttpGet("Products")]
        public IActionResult GetAllProducts()
        {
            var productList = _productOperations.GetAllProducts();
            if (productList != null && productList.ToList().Count > 0)
            {
                return Ok(productList);
            }
            return NotFound("No products available at the moment.");
        }

        [HttpGet("Products/{id}")]
        [RouteParameterValidation("id")]
        public async Task<IActionResult> GetProductWithProductId(int id)
        {
            var productResponse = await _productOperations.GetProductWithProductId(id);
            if (productResponse == null)
            {
                return NotFound("Product not found");
            }
            return Ok(productResponse);
        }

        [HttpPost("Products")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }
            var productResponse = await _productOperations.AddProduct(product);
            if(productResponse == null)
            {
                return BadRequest("Product already exists. Please try adding another product.");
            }
            return Ok(productResponse);
        }

        [HttpDelete("Products/{id}")]
        [RouteParameterValidation("id")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var productResponse = await _productOperations.DeleteProductById(id);
            if (productResponse == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(productResponse);
        }

        [HttpPut("Products/{id}")]
        [RouteParameterValidation("id")]
        public async Task<IActionResult> UpdateProductById(int id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }
            var productResponse = await _productOperations.UpdateProductById(id, product);
            return Ok(productResponse);
        }

        [HttpPut("Products/Decrement-Stock/{id}/{quantity}")]
        [RouteParameterValidation("id")]
        [RouteParameterValidation("quantity")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            var response = await _productOperations.DecrementStock(id, quantity);
            if (response == null)
            {
                return NotFound("Product not found");
            }
            return Ok(response);
        }

        [HttpPut("Products/Add-To-Stock/{id}/{quantity}")]
        [RouteParameterValidation("id")]
        [RouteParameterValidation("quantity")]
        public async Task<IActionResult> IncrementStock(int id, int quantity)
        {
            var response = await _productOperations.IncrementStock(id, quantity);
            if (response == null)
            {
                return NotFound("Product not found");
            }
            return Ok(response);
        }
    }
}