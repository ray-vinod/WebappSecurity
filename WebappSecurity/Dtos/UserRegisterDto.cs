using System.ComponentModel.DataAnnotations;

namespace WebappSecurity.Dtos;
public class UserRegisterStep1Dto
{
    public int TabIndex { get; set; } = 0;

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

    [StringLength(55, MinimumLength = 3)]
    public string? Address { get; set; }
}