using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MyShop.Data;
using MyShop.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MyShop.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Laukas 'El. paštas' yra privalomas")]
            [EmailAddress]
            [Display(Name = "El. paštas")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Laukas 'Slaptažodis' yra privalomas")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Slaptažodis")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Laukas 'Pakartokite slaptažodį' yra privalomas")]
            [DataType(DataType.Password)]
            [Display(Name = "Pakartokite slaptažodį")]
            [Compare("Password", ErrorMessage = "Slaptažodžiai nesutampa.")]
            public string ConfirmPassword { get; set; }

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
            public string City { get; set; }

            [Display(Name = "Rolė")]
            public string Role { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ViewData["roles"] = _roleManager.Roles.Where(r => r.Name == "User").ToList();
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            List<CityModel> availableCities = _context.Cities.ToList();
            Cities = availableCities.Select(x => new SelectListItem() { Value = x.ID, Text = x.Name }).ToList();
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            var role = _roleManager.FindByIdAsync(Input.Role).Result;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Address = Input.Address,
                    CityID = _context.Cities.Where(x => x.ID == Input.City).FirstOrDefault().ID
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, role.Name);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code, returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                    else
                    {
                        if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                        {
                            return RedirectToAction("ListUsers", "Role");
                        }
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            List<CityModel> availableCities = _context.Cities.ToList();
            Cities = availableCities.Select(x => new SelectListItem() { Value = x.ID, Text = x.Name }).ToList();
            ViewData["roles"] = _roleManager.Roles.ToList();
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
