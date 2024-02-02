using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using WebappSecurity.Constants.Enums;

namespace WebappSecurity.Models.Identity;
public class AppUser : IdentityUser
{
    [Required]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public string? Country { get; set; }

    [Required]
    public string? CountryCode { get; set; }

    public string? State { get; set; }

    public string? PhoneCode { get; set; }

    public string? ImagePath { get; set; }
}