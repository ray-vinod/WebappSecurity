using System.ComponentModel.DataAnnotations;

namespace WebappSecurity.Models;
public class Credential
{
    [Required]
    [Display(Name = "User Name")]
    [DataType(DataType.EmailAddress)]
    public string? UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}