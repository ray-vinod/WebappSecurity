using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebappSecurity.Dtos;
using WebappSecurity.Models.Identity;

namespace WebappSecurity.Pages.Account;
public class StepRegisterModel(UserManager<AppUser> userManager) : PageModel
{
    private readonly UserManager<AppUser> _userManager = userManager;

    [BindProperty]
    public UserRegisterDto Input { get; set; } = new();

    [BindProperty]
    public UserProfileDto Profile { get; set; } = new();

    [BindProperty]
    public int TabIndex { get; set; }
    public string? ReturnUrl { get; set; }



    public void OnGet() { }


    public async Task<IActionResult> OnPostRegisterAsync(string? returnUrl = null, int tabindex = 0)
    {
        TabIndex = tabindex;
        ReturnUrl = returnUrl ?? "/Acccount/StepRegister";

        var keys = ModelState.Keys;
        foreach (var key in keys)
        {
            if (key != "Input.Email" && key != "Input.Password")
            {
                var input = ModelState.Where(x => x.Key == key).FirstOrDefault();
                input.Value!.Errors.Clear();
                input.Value.ValidationState = ModelValidationState.Valid;
            }
        }

        if (!ModelState.IsValid) return RedirectToPage(ReturnUrl);

        AppUser newUser = new() { Email = Input.Email, UserName = Input.Email };

        var result = await _userManager.CreateAsync(newUser, Input.Password!);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                string errorCode = error.Code.Contains("Password") ? "Password"
                : error.Code.Contains("UserName") ? "Email"
                : "";
                ModelState.AddModelError($"Input.{errorCode}", error.Description);
            }

            return RedirectToPage(ReturnUrl); ;
        }


        return RedirectToPage(ReturnUrl); ;
    }

    public IActionResult OnPostUpdateAsync(int tabindex = 0)
    {
        TabIndex = tabindex;
        return RedirectToPage("/Index");
    }
}