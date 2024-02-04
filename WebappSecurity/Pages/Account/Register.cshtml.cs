using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebappSecurity.Dtos;
using WebappSecurity.Models.Identity;

namespace WebappSecurity.Pages.Account;
public class RegisterModel(UserManager<AppUser> userManager) : PageModel
{
    private readonly UserManager<AppUser> _userManager = userManager;

    [BindProperty]
    public RegisterDto Input { get; set; } = new();

    [BindProperty]
    public ProfileDto Profile { get; set; } = new();

    [BindProperty]
    public int Initialtab { get; set; }

    [BindProperty]
    public string CurrentUser { get; set; } = "";

    public string? ReturnUrl { get; set; }


    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostRegisterAsync(int tabindex, string? returnUrl)
    {
        Initialtab = tabindex;
        ReturnUrl = returnUrl;

        Error([.. ModelState.Keys], "Input", ["Email", "Password"]);

        if (!ModelState.IsValid)
        {
            Initialtab--;
            return Page();
        }

        AppUser appUser = new()
        {
            FirstName = "",
            LastName = "",
            Email = Input.Email,
            UserName = Input.Email,
            Country = "",
            CountryCode = ""
        };

        var result = await _userManager.CreateAsync(appUser, Input.Password!);
        if (!result.Succeeded)
        {
            Error(result, "Input", ["DuplicateUserName", "Email"]);

            Initialtab--;
            return Page();
        }

        CurrentUser = appUser.Email!;

        return Page(); ;
    }

    public async Task<IActionResult> OnPostUpdateAsync(int tabindex, string? returnUrl)
    {
        Initialtab = tabindex;
        ReturnUrl = returnUrl;

        Error([.. ModelState.Keys], "Profile", ["FirstName", "LastName"]);

        if (!ModelState.IsValid)
        {
            Initialtab--;
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(CurrentUser);

        if (user == null)
        {
            Initialtab--;
            return Page();
        }

        user.FirstName = Profile.FirstName!;
        user.LastName = Profile.LastName!;
        user.Gender = Profile.Gender;
        user.PhoneNumber = Profile.Phone;
        user.PhoneCode = Profile.PhoneCode;
        user.Country = Profile.Country;
        user.CountryCode = Profile.CountryCode;
        user.State = Profile.State;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            Error(result, "Profile", ["FirstName", "LastName"]);

            Initialtab--;
            return Page();
        }

        CurrentUser = user.Email!;
        return Page();
    }

    public async Task<IActionResult> OnPostUploadAsync(int tabindex, string? returnUrl)
    {
        ReturnUrl = returnUrl;
        Initialtab = tabindex;

        Error([.. ModelState.Keys], "Profile", ["ProfileImage"]);

        if (!ModelState.IsValid)
        {
            Initialtab--;
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(CurrentUser);
        if (user == null)
        {
            Initialtab--;
            return Page();
        }

        user.ImagePath = "";

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            Error(result, "Profile", ["FirstName", "LastName"]);

            Initialtab--;
            return Page();
        }

        return RedirectToPage("/account/login");
    }

    // Error generate for this fields 'errorFor'
    private void Error(IdentityResult? result, string prefix, string[] errorFor)
    {
        foreach (var error in result!.Errors)
        {
            if (errorFor.Any(x => x.Contains(error.Code)))
            {
                if (error.Code == "DuplicateUserName")
                {
                    ModelState.AddModelError($"{prefix}.Email", error.Description);
                }
                else
                {
                    ModelState.AddModelError($"{prefix}.{error.Code}", error.Description);
                }
            }
        }
    }

    //Error Clear except this fields 'exceptKeys'
    private void Error(string[] keys, string prefix, string[] exceptKeys)
    {
        foreach (var key in keys)
        {
            if (!exceptKeys.Any(x => $"{prefix}.{x}".Contains(key)))
            {
                var field = ModelState[key];
                field!.Errors.Clear();
                field!.ValidationState = ModelValidationState.Valid;
            }
        }
    }

}