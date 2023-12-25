using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebappSecurity.Models;

namespace WebappSecurity.Pages.Account;
public class LoginModel : PageModel
{

    [BindProperty]
    public Credential Credential { get; set; } = new();

    public string? ReturnURL { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync(string? returnURL = null)
    {
        ReturnURL = returnURL ?? "/Index";

        if (!ModelState.IsValid) return Page();

        if (Credential.UserName == "admin@info.com" && Credential.Password == "password")
        {
            // claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email,Credential.UserName),
                new(ClaimTypes.Name,Credential.UserName),
                new(ClaimTypes.Role,"Admin"),
            };

            // create identity
            var identity = new ClaimsIdentity(claims, "appCookie");

            // identity principle
            var principle = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = Credential.RememberMe,
                RedirectUri = ReturnURL
            };

            await HttpContext.SignInAsync("appCookie", principle, authProperties);
        }
        else
        {
            ModelState.AddModelError("", "Invalid (user Name/Password) login attempt");
        }

        return Page();
    }
}