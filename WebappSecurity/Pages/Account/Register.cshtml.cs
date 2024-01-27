using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebappSecurity.Dtos;
using WebappSecurity.Models.Identity;

namespace WebappSecurity.Pages.Account;
public class RegisterModel(UserManager<AppUser> userManager) : PageModel
{
    private readonly UserManager<AppUser> _userManager = userManager;

    [BindProperty]
    public RegisterDto Input { get; set; } = new();

    public string? ReturnURL { get; set; }

    public IActionResult OnGet(string? returnUrl = null)
    {
        ReturnURL = returnUrl ?? "/Index";

        return Page();
    }

    public async Task<IActionResult> OnPostUserAsync(string? returnUrl = null, int? tabindex = 0)
    {
        ReturnURL = returnUrl ?? "/Account/Login";

        if (!ModelState.IsValid) return Page();

        var user = new AppUser()
        {
            FirstName = Input.FirstName!,
            LastName = Input.LastName!,
            Email = Input.Email,
            UserName = Input.Email,
        };

        var result = await _userManager.CreateAsync(user, Input.Password!);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                var errorCode = error.Code.Contains("Password") ? "Password" : "";
                ModelState.AddModelError("Input." + errorCode, error.Description);
            }

            return Page();
        }

        return RedirectToPage(ReturnURL);
    }
}