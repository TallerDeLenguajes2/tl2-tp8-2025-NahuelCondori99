using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_NahuelCondori99.Interfaces;
using tl2_tp8_2025_NahuelCondori99.Models;

public class AccountController : Controller
{
    private readonly IAuthenticationService auth;

    public AccountController(IAuthenticationService auth)
    {
        this.auth = auth;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var user = auth.Login(vm.Username, vm.Password);

        if (user == null)
        {
            ViewBag.Error = "Usuario o contrasenia incorrectos";
            return View(vm);
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        auth.Logout();
        return RedirectToAction("Login");
    }
}