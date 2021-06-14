using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }

        [Required]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "El. paštas")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Vardas")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Pavardė")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Adresas")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Miestas")]
        public string CityID { get; set; }

        [ForeignKey("CityID")]
        public CityModel City { get; set; }

        [Required]
        [Display(Name = "Pretenzijos")]
        public List<string> Claims { get; set; }

        [Required]
        [Display(Name = "Rolės")]
        public List<string> Roles { get; set; }
    }
}
