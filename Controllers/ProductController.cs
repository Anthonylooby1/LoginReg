using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginReg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LoginReg.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;

    private MyContext _context;  

    public ProductController(ILogger<ProductController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;
    }

    [HttpGet("add/product")]
    public IActionResult AddProductView()
    {
        return View("Product");
    }

    [HttpPost("create/product")]
    public IActionResult CreateProduct(Product newProduct)
    {
        if(!ModelState.IsValid)
        {
            return View("Product");
        }
        newProduct.UserId = (int)HttpContext.Session.GetInt32("UUID");
        _context.Add(newProduct);
        _context.SaveChanges();
        return RedirectToAction("Admin", "User");
    }

    [HttpPost("delete/{id}")]
    public IActionResult Delete(int id)
    {
        Product? deleted = _context.Products.SingleOrDefault(p => p.ProductId == id);
        if(deleted != null)
        {
            _context.Remove(deleted);
            _context.SaveChanges();
        }
        return RedirectToAction("Home", "User");
    }

    [HttpGet("product/{productId}")]
    public IActionResult ViewProduct(int productId)
    {
        Product? OneProduct = _context.Products.FirstOrDefault(p => p.ProductId == productId);
        if(OneProduct == null)
        {
            return RedirectToAction("Home");
        }
        return View(OneProduct);
    }
}    