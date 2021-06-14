using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class CategoryModel
    {
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "Laukas 'ID' yra privalomas")]
        public int ID { get; set; }

        [Display(Name = "Pavadinimas")]
        [Required(ErrorMessage = "Laukas 'Pavadinimas' yra privalomas")]
        public string Title { get; set; }

        [Display(Name = "Aprašymas")]
        public string Description { get; set; }

        public int? ParentID { get; set; }

        [ForeignKey("ParentID")]
        public CategoryModel Parent { get; set; }
    }
}
