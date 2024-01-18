using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebappSecurity.Constants;
using WebappSecurity.Dtos;
using WebappSecurity.Models.Identity;

namespace WebappSecurity.Pages.Account;
public class LoginModel(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager) : PageModel
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    [BindProperty]
    public CredentialDto Input { get; set; } = new();

    public string? ReturnURL { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(string? returnURL = null)
    {
        ReturnURL = returnURL ?? "/home";

        if (!ModelState.IsValid) return Page();

        var user = await _userManager.FindByEmailAsync(Input.UserName!);
        if (user == null)
        {
            ModelState.AddModelError("Input.UserName", "Invalid user name or email");
            return Page();
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, Input.Password!);
        if (!isValidPassword)
        {
            ModelState.AddModelError("Input.Password", "Invalid Password");
            return Page();
        }

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = Input.RememberMe,
            RedirectUri = ReturnURL,
        };

        _signInManager.AuthenticationScheme = Config.SchemeName;
        await _signInManager.SignInAsync(user, authProperties);

        return Page();
    }


}