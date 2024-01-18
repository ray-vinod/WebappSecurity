using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebappSecurity.Dtos;

namespace WebappSecurity.Pages.Account;
public class StepLogin : PageModel
{
    [BindProperty]
    public CredentialDto Input { get; set; } = new();
}