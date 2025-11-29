using tl2_tp8_2025_NahuelCondori99.Interfaces;
using tl2_tp8_2025_NahuelCondori99.Models;

namespace tl2_tp8_2025_NahuelCondori99.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticationService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool Login(string username, string password)
        {
            var context = _httpContextAccessor.HttpContext;
            var user = _userRepository.GetUsuario(username, password);
            if (user != null)
            {
                if (context == null) 
                {
                    throw new InvalidOperationException("HttpContext no disponible");
                }
                context.Session.SetString("IsAuthenticated", "true");
                context.Session.SetString("User", user.User);
                context.Session.SetString("UserNombre", user.Nombre);
                context.Session.SetString("Rol", user.Rol);
                return true;
            }
            return false;
        }

        public void Logout()
        {
            var context = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext no disponible");
            context.Session.Clear();
        }

        public bool IsAuthenticated()
        {
            var context = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext no disponible");
            return context.Session.GetString("IsAuthenticated") == "true";
        }

        public bool HasAccessLevel(string requiredAccessLevel)
        {
            var context = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext no disponible");
            return context.Session.GetString("Rol") == requiredAccessLevel;
        }
    }
}