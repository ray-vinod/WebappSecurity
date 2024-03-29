using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using WebappSecurity.Dtos;
using WebappSecurity.Models.Identity;

namespace WebappSecurity.Pages.Account;
public class RegisterModel(UserManager<AppUser> userManager, IWebHostEnvironment env) : PageModel
{
    #region privateField
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IWebHostEnvironment _env = env;

    [BindProperty]
    public RegisterDto Input { get; set; } = new();

    [BindProperty]
    public ProfileDto Profile { get; set; } = new();

    [BindProperty]
    public int Initialtab { get; set; }

    [BindProperty]
    public string CurrentUser { get; set; } = "";

    public string? ReturnUrl { get; set; }

    #endregion

    public IActionResult OnGet()
    {
        return Page();
    }

    #region Register
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
            // if duplicate user attempt, then
            var user = await _userManager.FindByEmailAsync(Input.Email!);
            var validPassword = await _userManager.CheckPasswordAsync(user!, Input.Password!);
            if (validPassword)
            {
                CurrentUser = user?.Email!;
                return Page();
            }

            Error(result, "Input", ["DuplicateUserName", "Email"]);

            Initialtab--;
            return Page();
        }

        CurrentUser = appUser.Email!;

        return Page(); ;
    }
    #endregion

    #region Update
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
    #endregion

    #region Upload
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
        CurrentUser = "vinod@gmail.com";
        var user = await _userManager.FindByEmailAsync(CurrentUser);
        if (user == null)
        {
            Initialtab--;
            return Page();
        }

        Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "images"));
        var fileName = $"avatar_{user!.Email}.png";
        var path = Path.Combine(_env.WebRootPath, "images", fileName);

        using var image = Image.Load(Profile.ProfileImage!.OpenReadStream());
        image.Mutate(x => x.Resize(150, 150));
        image.SaveAsPng(path);
        user.ImagePath = "/images/" + fileName;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            Error(result, "Profile", ["ProfileImage"]);

            Initialtab--;
            return Page();
        }

        return RedirectToPage("/account/login");
    }
    #endregion

    #region Error
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
    #endregion
}