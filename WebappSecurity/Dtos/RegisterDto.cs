using System.ComponentModel.DataAnnotations;

namespace WebappSecurity.Dtos;
public class RegisterDto
{
    [Required]
    [Display(Name = "First Name")]
    [StringLength(55, MinimumLength = 3)]
    public string? FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    [StringLength(55, MinimumLength = 3)]
    public string? LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(20)]
    public string? Password { get; set; }

    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; }
}