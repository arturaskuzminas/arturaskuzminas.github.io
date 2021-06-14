using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class CityModel
    {
        [Key]
        [Required(ErrorMessage = "Laukas 'Miesto kodas' yra privalomas")]
        [Display(Name = "Miesto kodas")]
        public string ID { get; set; }

        [Required(ErrorMessage = "Laukas 'Miesto pavadinimas' yra privalomas")]
        [Display(Name = "Miesto pavadinimas")]
        public string Name { get; set; }
    }
}
