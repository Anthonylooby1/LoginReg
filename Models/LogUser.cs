#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginReg.Models;

public class LogUser 
{
    [Required]
    [Display(Name = "EmployeeId")]
    public int LogEmployeeId {get;set;}

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string LogPassword {get;set;}

}