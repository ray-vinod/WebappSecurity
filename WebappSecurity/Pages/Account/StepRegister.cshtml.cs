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


    public void OnGet()
    {
        TabIndex = 1;
        Profile.Email = "vinod@gmail.com";
    }

    public async Task<IActionResult> OnPostRegisterAsync(string? returnUrl = null, int tabindex = 0)
    {
        TabIndex = tabindex;
        ReturnUrl = returnUrl;

        //model errors clear excepts the current form's model
        Error([.. ModelState.Keys], "Input", ["Email", "Password"]);

        if (!ModelState.IsValid)
        {
            Tabs();
            return Page();
        }

        AppUser newUser = new() { Email = Input.Email, UserName = Input.Email };

        var result = await _userManager.CreateAsync(newUser, Input.Password!);
        if (!result.Succeeded)
        {
            // there is only dubplicate user name error
            Error(result, "Input", ["UserName"]);
            Tabs();
            return Page();
        }

        //  this is set for next form to find the current user
        Profile.Email = newUser.Email;

        ViewData["error"] = "";
        return Page(); ;
    }

    public async Task<IActionResult> OnPostUpdateAsync(string? returnUrl = null, int tabindex = 0)
    {
        TabIndex = tabindex;
        ReturnUrl = returnUrl;

        //model errors clear excepts the current form's model
        Error([.. ModelState.Keys], "Profile", ["FirstName", "LastName"]);

        if (!ModelState.IsValid)
        {
            Tabs();
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Profile.Email!);

        if (user == null)
        {
            Tabs();
            return Page();
        }

        // if (!string.IsNullOrEmpty(Profile.FirstName) &&
        //     !string.IsNullOrEmpty(Profile.LastName) &&
        //     !string.IsNullOrEmpty(Profile.Gender.ToString()))
        // {
        //     return Page();
        // }

        user.FirstName = Profile.FirstName!;
        user.LastName = Profile.LastName!;
        user.Gender = Profile.Gender;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            Error(result, "Profile", ["FirstName", "LastName", "Gender"]);
            Tabs();
            return Page();
        }

        return RedirectToPage("/account/login");
    }


    public async Task<IActionResult> OnPostUploadAsync()
    {
        await Task.Delay(0);

        return Page();
    }

    private void Tabs()
    {
        TabIndex--;
        ViewData["error"] = TabIndex;
    }

    // Error generate for this fields 'errorFor'
    private void Error(IdentityResult? result, string prefix, string[] errorFor)
    {
        foreach (var error in result!.Errors)
        {
            string errorCode = "";
            foreach (var key in errorFor)
            {
                errorCode = error.Code.Contains(key) ? key : "";
                if (key == "UserName")
                {
                    errorCode = error.Code.Contains(key) ? "Email" : "";
                }

                ModelState.AddModelError($"{prefix}.{errorCode}", error.Description);
            }
        }
    }

    //Error Clear except this fields 'exceptKeys'
    private void Error(string[] keys, string prefix, string[] exceptKeys)
    {
        foreach (var key in keys)
        {
            foreach (var eKey in exceptKeys)
            {
                if (key != $"{prefix}.{eKey}" && key != $"{prefix}.{eKey}")
                {
                    var input = ModelState.Where(x => x.Key == key).FirstOrDefault();
                    input.Value!.Errors.Clear();
                    input.Value.ValidationState = ModelValidationState.Valid;
                }
            }
        }
    }

}