using VF.Patika_W2.API.Model.Entitiy;

namespace VF.Patika_W2.API.Extension
{
    public static class ProductExtensions
    {
        public static IQueryable<Product> FilterByName(this IQueryable<Product> products, string name)
        {
            return !string.IsNullOrEmpty(name) ? products.Where(p => p.Name.Contains(name)) : products;
        }
    }
}
