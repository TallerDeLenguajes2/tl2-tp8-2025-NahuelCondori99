namespace tl2_tp8_2025_NahuelCondori99.Interfaces
{
    public interface IProductoRepository
    {
        List<Productos> GetAll();
        Productos GetById(int id);
        void Alta(Productos p);
        bool Modificar(int id, Productos p);
        bool Eliminar(int id);
    }

}