using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebappSecurity.Dtos;

namespace WebappSecurity.Pages.Account;
public class TestModel : PageModel
{

    [BindProperty]
    public UserRegisterDto Input { get; set; } = new();

    [BindProperty]
    public int TabIndex { get; set; } = 0;

    public void OnGet() { }

    public void OnPost() { }

    public IActionResult OnPostRegister(string? returnUrl = null, int tabindex = 0)
    {

        if (ModelState.IsValid) return Page();

        return Page();
    }
}