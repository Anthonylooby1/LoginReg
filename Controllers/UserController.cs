using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginReg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace LoginReg.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    private MyContext _context;  

    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [SessionCheck]
    [HttpGet("home")]
    public IActionResult Home()
    {
        return View("Home");
    }

    [HttpPost("users/register")]
    public IActionResult RegisterUser(User newUser)
    {
        if(!ModelState.IsValid) {
            return View("Index");
        }
        PasswordHasher<User> hashing = new();
        newUser.Password = hashing.HashPassword(newUser, newUser.Password);
        _context.Add(newUser);
        _context.SaveChanges();
        HttpContext.Session.SetInt32("UUID", newUser.UserId);
        return RedirectToAction("Home");
    }

    [HttpPost("users/login")]
    public IActionResult LoginUser(LogUser logAttempt)
    {
        if(!ModelState.IsValid)
        {
            return View("Index");
        }
        User? dbUser = _context.Users.FirstOrDefault(u => u.Email == logAttempt.LogEmail);
        if(dbUser == null)
        {
            ModelState.AddModelError("LogPassword", "Invalid Login Attempt");
            return View("Index");
        }
        PasswordHasher<LogUser> hashing = new();
        PasswordVerificationResult pwCompareResult = hashing.VerifyHashedPassword(logAttempt, dbUser.Password,logAttempt.LogPassword);
        if(pwCompareResult == 0)
        {
           ModelState.AddModelError("LogPassword", "Invalid Login Attempt");
        return View("Index"); 
        }
        HttpContext.Session.SetInt32("UUID", dbUser.UserId);
        return RedirectToAction("Home");
    }

    [HttpPost("user/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UUID");
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
