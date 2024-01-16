using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebappSecurity.Models.Identity;
public class AppUser : IdentityUser
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

}