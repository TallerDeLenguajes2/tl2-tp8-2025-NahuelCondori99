using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_NahuelCondori99;
using tl2_tp8_2025_NahuelCondori99.Interfaces;
using tl2_tp8_2025_NahuelCondori99.ViewModels;
using tl2_tp8_2025_NahuelCondori99.Models;

public class LoginController : Controller
{
    private readonly IAuthenticationService _auth;
    private readonly ILogger<LoginController> _logger;

    public LoginController(IAuthenticationService auth, ILogger<LoginController> logger)
    {
        _auth = auth;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = new LoginViewModel()
        {
            IsAuthenticaded = HttpContext.Session.GetString("IsAuthenticated") == "true"
        };
        return View(model);
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
        vm.IsAuthenticaded = false;
        return View("Index", vm);
    }

    public IActionResult Logout()
    {
        _auth.Logout();
        return RedirectToAction("Index");
    }
}