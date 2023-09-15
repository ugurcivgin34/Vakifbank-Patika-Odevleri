using System.ComponentModel.DataAnnotations;

namespace VF.Patika_W2.API.Model.Entitiy
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
