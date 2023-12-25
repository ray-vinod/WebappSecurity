
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebappSecurity.Pages.Account;
public class LogoutModel : PageModel
{
    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        await HttpContext.SignOutAsync("appCookie");
        return RedirectToPage("/Index");
    }
}