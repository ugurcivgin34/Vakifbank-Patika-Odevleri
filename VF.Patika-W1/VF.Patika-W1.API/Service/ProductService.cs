using VF.Patika_W1.API.Model.Entity;

namespace VF.Patika_W1.API.Service
{
    public class ProductService : IProductService
    {
        private List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Product A" },
        new Product { Id = 2, Name = "Product B" }
    };

        public IEnumerable<Product> ListProducts(string name)
        {
            var products = _products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            return products.OrderBy(p => p.Name).ToList();
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product AddProduct(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
            return product;
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
        public IEnumerable<Product> SortProducts(string sortBy)
        {
            var products = _products.AsQueryable();

            if (string.IsNullOrEmpty(sortBy))
            {
                sortBy = "name"; // default sıralama
            }

            switch (sortBy.ToLower())
            {
                case "name":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "id":
                    products = products.OrderBy(p => p.Id);
                    break;
                default:
                    throw new ArgumentException("Invalid sort value");
            }

            return products.ToList();
        }

    }
}
