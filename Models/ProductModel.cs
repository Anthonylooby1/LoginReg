#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LoginReg.Models;

public class Product
{
    [Key]
    public int ProductId {get;set;}

    [Required]
    public string Name {get;set;}

    [Required]
    public string Description {get;set;}

    [Required]
    public string ImgURL {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;


    public int UserId {get;set;}
    public User? OneUser {get;set;}

    

}