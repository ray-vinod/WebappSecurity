using System.ComponentModel.DataAnnotations;

namespace WebappSecurity.Dtos;
public class UserRegisterStep1Dto
{
    [Required]
    [Display(Name = "First Name")]
    [StringLength(55, MinimumLength = 3)]
    public string? FirstName { get; set; }
}

public class UserRegisterStep2Dto
{
    [Required]
    [Display(Name = "Last Name")]
    [StringLength(55, MinimumLength = 3)]
    public string? LastName { get; set; }
}