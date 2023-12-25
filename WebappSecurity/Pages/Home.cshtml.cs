using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebappSecurity.Pages;

[Authorize]
public class HomeModel : PageModel
{
    public void OnGet()
    {

    }
}