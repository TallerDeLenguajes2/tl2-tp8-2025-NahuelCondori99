namespace tl2_tp8_2025_NahuelCondori99.Interfaces
{
    public interface IUserRepository
    {
        Usuario GetUsuario(string username, string password);
    }    
}
