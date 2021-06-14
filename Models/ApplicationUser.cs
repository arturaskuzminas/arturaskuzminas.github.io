using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MyShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Miestas")]
        public string CityID { get; set; }

        [ForeignKey("CityID")]
        [Display(Name = "Miestas")]
        public CityModel City { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}
