using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class ProductModel
    {
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "Laukas 'ID' yra privalomas")]
        public int ID { get; set; }

        [Display(Name = "Pavadinimas")]
        [Required(ErrorMessage = "Laukas 'Pavadinimas' yra privalomas")]
        public string Title { get; set; }

        [Display(Name = "Kaina")]
        [Required(ErrorMessage = "Laukas 'Kaina' yra privalomas")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Display(Name = "Brand'as")]
        public string Brand { get; set; }

        [Display(Name = "Dydis")]
        public string Size { get; set; }

        [Display(Name = "Lytis")]
        public string ForSex { get; set; }

        [Display(Name = "Spalva")]
        public string Color { get; set; }

        [Display(Name = "Likutis")]
        [Required(ErrorMessage = "Laukas 'Likutis' yra privalomas")]
        public int StockCount { get; set; }

        [Display(Name = "Nuotr. link'as")]
        [Required]
        public string PictureLink { get; set; }

        [Display(Name = "Perkamiausia")]
        [Required]
        public bool MostWanted { get; set; }

        [Display(Name = "KategorijosID")]
        [Required(ErrorMessage = "Laukas 'Kategorija' yra privalomas")]
        public int CategoryID { get; set; }

        [Display(Name = "Kategorija")]
        [ForeignKey("CategoryID")]
        public CategoryModel Category { get; set; }

        [Display(Name = "Aprašymas")]
        public string Description { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Categories { get; set; }


    }
}
