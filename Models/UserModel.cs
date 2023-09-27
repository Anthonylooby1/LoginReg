#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginReg.Models;

public class User 
{
    [Key]
    public int UserId {get;set;}

    [Required]
    public int EmployeeId {get;set;}

    [Required]
    public int Level {get;set;}

    [Required]
    public string Name {get;set;}

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    [NotMapped]
    [Compare("Password")]
    public string ConfirmPassword {get;set;}

    public List<Product> AllProducts {get;set;} = new List<Product>();

}




