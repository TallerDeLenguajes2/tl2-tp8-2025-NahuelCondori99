using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_NahuelCondori99;
using tl2_tp8_2025_NahuelCondori99.Interfaces;
using tl2_tp8_2025_NahuelCondori99.ViewModels;
using tl2_tp8_2025_NahuelCondori99.Models;

public class LoginController : Controller
{
    private readonly IAuthenticationService _auth;

    public LoginController(IAuthenticationService auth)
    {
        _auth = auth;
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel vm)
    {
        if (string.IsNullOrEmpty(vm.Username) || string.IsNullOrEmpty(vm.Password))
        {
            vm.ErrorMessage = "Debe ingresar usuario y contraseña";
            return View("Index", vm);
        }

        if (_auth.Login(vm.Username, vm.Password))
        {
            return RedirectToAction("Index", "Home");
        }

        vm.ErrorMessage = "Credenciales invalidas";
        return View("Index", vm);
    }

    public IActionResult Logout()
    {
        _auth.Logout();
        return RedirectToAction("Index");
    }
}