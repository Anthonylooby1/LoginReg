#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginReg.Models;

public class LogUser 
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string LogEmail {get;set;}

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string LogPassword {get;set;}
}