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
                return NotFound();
            }
            return Ok(product);
        }

        [Authorize (Roles = "Administrator,Staff")]
        [HttpPost]
        public ActionResult AddProduct([FromBody] Product product)
        {
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
                return NotFound();
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
                return NotFound();
            }

            _productServices.DeleteProduct(id);
            return NoContent();
        }
        
    }
}