using Microsoft.AspNetCore.Mvc;
using services;
using models;
using Microsoft.AspNetCore.Authorization;

namespace controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductControllers : ControllerBase
    {
        private readonly ProductServices _productServices;

        public ProductControllers()
        {
            _productServices = new ProductServices();
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            var products = _productServices.LoadProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductByID(int id)
        {
            var product = _productServices.GetProductByID(id);
            if (product == null)
            {
                return NotFound("Product doesn't exist.");
            }
            return Ok(product);
        }

        [Authorize (Roles = "Administrator,Staff")]
        [HttpPost]
        public ActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return NotFound("Product doesn't exist.");
            }

            if(product.ID <= 0)
            {
                return BadRequest("ID must be greater than zero");
            }

            if(product.Name == null || product.Name == "")
            {
                return BadRequest("Product Name is required");
            }

            if(product.Description == null || product.Description == "")
            {
                return BadRequest("Product Description is required");
            }

            if(product.Price <= 0)
            {
                return BadRequest("Product Price must be greater than zero");
            }

            if(product.StockQuantity < 0)
            {
                return BadRequest("Product Stock Quantity cannot be negative");
            }


            _productServices.AddProduct(product);
            return CreatedAtAction(nameof(GetProductByID), new { id = product.ID }, product);
        }   

        [Authorize (Roles = "Administrator,Staff")]
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = _productServices.GetProductByID(id);
            if (existingProduct == null)
            {
                return NotFound("Product doesn't exist.");
            }

                      if(product.Name == null || product.Name == "")
            {
                return BadRequest("Product Name is required");
            }

            if(product.Description == null || product.Description == "")
            {
                return BadRequest("Product Description is required");
            }

            if(product.Price <= 0)
            {
                return BadRequest("Product Price must be greater than zero");
            }

            if(product.StockQuantity < 0)
            {
                return BadRequest("Product Stock Quantity cannot be negative");
            }

            product.ID = id; 
            _productServices.UpdateProduct(product);
            return NoContent();
        }

        [Authorize (Roles = "Administrator,Staff")]
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var existingProduct = _productServices.GetProductByID(id);
            if (existingProduct == null)
            {
                return NotFound("Product doesn't exist.");
            }

            _productServices.DeleteProduct(id);
            return NoContent();
        }
        
    }
}