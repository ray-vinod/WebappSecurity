using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebappSecurity.Dtos;

namespace WebappSecurity.Pages.Account;
public class StepLogin : PageModel
{
    [BindProperty]
    public UserRegisterStep1Dto Input { get; set; } = new();

    [BindProperty]
    public UserRegisterStep2Dto Input2 { get; set; } = new();



    public async Task<IActionResult> OnPostAsync()
    {
        await Task.Delay(0);

        if (!ModelState.IsValid) return Page();


        return Page();
    }
}