using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VF.Patika_W1.API.Model.Dtos;
using VF.Patika_W1.API.Model.Entity;
using VF.Patika_W1.API.Service;

namespace VF.Patika_W1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        private List<Product> _products = new List<Product> {
        new Product { Id = 1, Name = "Product A" },
        new Product { Id = 2, Name = "Product B" }
    };
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<ProductDTO>> List([FromQuery] string name)
        {
            var products = _products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            return products.Select(p => new ProductDTO { Id = p.Id, Name = p.Name }).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return new ProductDTO { Id = product.Id, Name = product.Name };
        }

        [HttpPost]
        public ActionResult<ProductDTO> Create(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _products.Add(product);

            return CreatedAtAction(nameof(Get), new { id = product.Id }, new ProductDTO { Id = product.Id, Name = product.Name });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = product.Name;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                return NotFound();

            patchDoc.ApplyTo(existingProduct, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                return NotFound();

            _products.Remove(existingProduct);

            return NoContent();
        }
    }
}
