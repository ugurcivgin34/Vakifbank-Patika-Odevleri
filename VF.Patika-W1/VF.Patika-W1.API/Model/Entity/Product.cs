using System.ComponentModel.DataAnnotations;

namespace VF.Patika_W1.API.Model.Entity
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
