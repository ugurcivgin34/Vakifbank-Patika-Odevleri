using VF.Patika_W1.API.Model.Entity;

namespace VF.Patika_W1.API.Service
{
    public interface IProductService
    {
        IEnumerable<Product> ListProducts(string name);
        Product GetProductById(int id);
        Product AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
