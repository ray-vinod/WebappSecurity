using System.ComponentModel.DataAnnotations;
using WebappSecurity.Constants.Enums;

namespace WebappSecurity.Dtos;
public class UserRegisterDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(20, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$",
        ErrorMessage = "In the valid password must have at least one capital letter, one special character and one digit")]
    public string? Password { get; set; }

    [Display(Name = "Re-Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; }
}

public class UserProfileDto
{
    public string? Email { get; set; }

    [Required]
    [Display(Name = "First Name")]
    [StringLength(55, MinimumLength = 3)]
    public string? FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    [StringLength(55, MinimumLength = 3)]
    public string? LastName { get; set; }

    public Gender Gender { get; set; }

    [Required]
    public string? Country { get; set; }

    public string? State { get; set; }

    [RegularExpression(@"^[+]?((\d{1,3})?[- .(]?)?(\d{3}[- .)]?){2}\d{4}$",
        ErrorMessage = "Invalid Phonenumber")]
    public string? Phone { get; set; }

    [Required]
    [Display(Name = "Profile Image")]
    public IFormFile? ProfileImage { get; set; }
}