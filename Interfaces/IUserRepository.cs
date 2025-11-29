namespace tl2_tp8_2025_NahuelCondori99.Models
{
    public interface IUserRepository
    {
        Usuario GetUsuario(string username, string password);
    }    
}
