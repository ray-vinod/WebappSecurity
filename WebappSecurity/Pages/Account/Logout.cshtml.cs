using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebappSecurity.Constants;
using WebappSecurity.Models.Identity;

namespace WebappSecurity.Pages.Account;
public class LogoutModel(SignInManager<AppUser> signInManager) : PageModel
{
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _signInManager.AuthenticationScheme = Config.SchemeName;
        await _signInManager.SignOutAsync();

        HttpContext.Response.Cookies.Delete(Config.CookieForgeryName);
        return RedirectToPage("/Index");
    }
}