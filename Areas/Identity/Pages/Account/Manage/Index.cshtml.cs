using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyShop.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Vardas")]
            [Required(ErrorMessage = "Laukas 'Vardas' yra privalomas")]
            public string FirstName { get; set; }

            [Display(Name = "Pavardė")]
            [Required(ErrorMessage = "Laukas 'Pavardė' yra privalomas")]
            public string LastName { get; set; }

            [Display(Name = "Adresas")]
            [Required(ErrorMessage = "Laukas 'Adresas' yra privalomas")]
            public string Address { get; set; }

            [Display(Name = "Miestas")]
            [Required(ErrorMessage = "Laukas 'Miestas' yra privalomas")]
            public string CityID { get; set; }

            [Phone]
            [Display(Name = "Telfono numeris")]
            [Required(ErrorMessage = "Laukas 'Telefono numeris' yra privalomas")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nepavyko užkrauti naudotojo su ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            user = await _userManager.GetUserAsync(User);

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                CityID = user.CityID,
                PhoneNumber = user.PhoneNumber
            };
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Nepavyko užkrauti naudotojo su ID '{_userManager.GetUserId(User)}'.");
            }
            else
            {
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.Address = Input.Address;
                user.CityID = Input.CityID;
                user.PhoneNumber = Input.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    StatusMessage = "Jūsų anketa buvo atnaujinta sėkmingai !";
                    return RedirectToPage();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return RedirectToPage();
            }
        }
    }
}
