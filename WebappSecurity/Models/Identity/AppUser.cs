using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using WebappSecurity.Constants.Enums;

namespace WebappSecurity.Models.Identity;
public class AppUser : IdentityUser
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public string? Country { get; set; }

    public string? State { get; set; }

    public string? Phone { get; set; }

}