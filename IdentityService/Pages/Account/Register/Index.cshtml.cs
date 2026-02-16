using Duende.IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace IdentityService.Pages.Account.Register
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class IndexModel(UserManager<ApplicationUser> userManager, ILogger<IndexModel> logger) : PageModel
    {

        [BindProperty]
        public required RegisterViewModel Input { get; set; }

        [BindProperty]
        public bool IsSuccessRegister { get; set; }

        public IActionResult OnGet(string returnUrl)
        {
            Input = new RegisterViewModel
            {
                ReturnUrl = returnUrl,
            };
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (Input.Button != "register")
                return Redirect(Input.ReturnUrl ?? "~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    IsSuccessRegister = true;
                    await userManager.AddClaimsAsync(user, new Claim[] { new Claim(JwtClaimTypes.Name, Input.FullName) });
                }
                else
                {
                    IsSuccessRegister = false;
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            else
            {
                logger.LogWarning("Invalid Inputs errors: {@Error}", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
            }
            return Page();
        }
    }
}
