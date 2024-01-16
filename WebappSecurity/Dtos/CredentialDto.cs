using System.ComponentModel.DataAnnotations;

namespace WebappSecurity.Dtos;
public class CredentialDto
{
    [Required]
    [Display(Name = "User Name")]
    [DataType(DataType.EmailAddress)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}