using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VF.Patika_W2.API.Attributes.Auth;
using VF.Patika_W2.API.Model.Dtos;
using VF.Patika_W2.API.Model.Entitiy;
using VF.Patika_W2.API.Service;

namespace VF.Patika_W2.API.Controllers
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


        /// <summary>
        /// Ürünleri listelemek ve filtrelemek için kullanılan endpoint.
        /// </summary>
        /// 
        [Auth]
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

        /// <summary>
        /// Ürünleri belirtilen kritere göre sıralamak için kullanılan endpoint.
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return new ProductDTO { Id = product.Id, Name = product.Name };
        }

        /// <summary>
        /// Yeni bir ürün ekleyen endpoint.
        /// </summary>
        /// <param name="product">Eklenen ürün.</param>
        /// <returns>Eklenen ürünün detayları.</returns>
        [HttpPost]
        public ActionResult<ProductDTO> Create(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _products.Add(product);

            return CreatedAtAction(nameof(Get), new { id = product.Id }, new ProductDTO { Id = product.Id, Name = product.Name });
        }

        /// <summary>
        /// Bir ürünü güncelleyen endpoint.
        /// </summary>
        /// <param name="id">Güncellenen ürünün ID'si.</param>
        /// <param name="product">Güncel ürün bilgileri.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = product.Name;

            return NoContent();
        }

        /// <summary>
        /// Bir ürünü kısmi olarak güncelleyen endpoint.
        /// </summary>
        /// <param name="id">Güncellenen ürünün ID'si.</param>
        /// <param name="patchDoc">JSON Patch belgesi ile güncellenen özellikler.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Belirtilen ürünü silen endpoint.
        /// </summary>
        /// <param name="id">Silinen ürünün ID'si.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                return NotFound();

            _products.Remove(existingProduct);

            return NoContent();
        }

        /// <summary>
        /// Ürünleri belirli bir kritere göre sıralayan endpoint.
        /// </summary>
        /// <param name="sortBy">Sıralama kriteri.</param>
        /// <returns>Sıralanmış ürün listesi.</returns>
        [HttpGet("sort")]
        public ActionResult<IEnumerable<ProductDTO>> Sort([FromQuery] string sortBy)
        {
            try
            {
                var products = _productService.SortProducts(sortBy);
                return products.Select(p => new ProductDTO { Id = p.Id, Name = p.Name }).ToList();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
