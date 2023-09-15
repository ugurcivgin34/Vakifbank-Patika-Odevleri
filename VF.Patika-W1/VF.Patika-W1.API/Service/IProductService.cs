using VF.Patika_W1.API.Model.Entity;

namespace VF.Patika_W1.API.Service
{
    public interface IProductService
    {
        /// <summary>
        /// İsim filtresine ve sıralama kriterine göre ürünleri listeler.
        /// </summary>
        /// <param name="name">İsim filtresi. Null veya boş olabilir.</param>
        /// <param name="sortBy">Sıralama kriteri. Null veya boş ise "name" varsayılan olarak seçilir.</param>
        /// <returns>Sıralanmış ve filtrelenmiş ürün listesi.</returns>
        IEnumerable<Product> ListProducts(string name);

        /// <summary>
        /// Ürünleri belirtilen kritere göre sıralar.
        /// </summary>
        /// <param name="sortBy">Sıralama kriteri. Null veya boş ise "name" varsayılan olarak seçilir.</param>
        /// <returns>Sıralanmış ürün listesi.</returns>
        IEnumerable<Product> SortProducts(string sortBy);

        /// <summary>
        /// Belirtilen ürünün detaylarını getirir.
        /// </summary>
        /// <param name="id">Ürün ID'si.</param>
        /// <returns>Ürün detayları.</returns>
        Product GetProductById(int id);

        /// <summary>
        /// Yeni bir ürün ekler.
        /// </summary>
        /// <param name="product">Eklenen ürün.</param>
        /// <returns>Eklenen ürün.</returns>
        Product AddProduct(Product product);

        /// <summary>
        /// Bir ürünü günceller.
        /// </summary>
        /// <param name="product">Güncel ürün bilgileri.</param>
        void UpdateProduct(Product product);

        /// <summary>
        /// Belirtilen ürünü siler.
        /// </summary>
        /// <param name="id">Silinen ürünün ID'si.</param>
        void DeleteProduct(int id);
    }
}
