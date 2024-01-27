using System.ComponentModel.DataAnnotations;

namespace WebappSecurity.Dtos;
public class UserRegisterDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(20, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$", ErrorMessage = "Your password contains at least One capital letter, one special character and one digit.")]
    public string? Password { get; set; }

    [Display(Name = "Re-Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; }
}

public class UserProfileDto
{
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

    [RegularExpression(@"^(\\+\\d{1,3}[- ]?)?\\d{10}$")]
    public string? Phone { get; set; }

    [Required]
    public IFormFile? ProfileImage { get; set; }
}

public enum Gender
{
    Male,
    Female,
    Others
}