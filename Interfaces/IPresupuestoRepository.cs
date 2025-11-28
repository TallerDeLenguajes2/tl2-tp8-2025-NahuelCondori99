namespace tl2_tp8_2025_NahuelCondori99.Interfaces
{
    public interface IPresupuestoRepository
    {
        List<Presupuestos> GetAll();
        Presupuestos GetById(int id);
        void crearPresupuesto(Presupuestos p);
        void AgregarProducto(int idPresupuesto, int idProducto, int cantidad);
        void ModificarCantidad(int idPresupuesto, int idProducto, int nuevaCantidad);
        void EliminarProducto(int idPresupuesto, int idProducto);
        bool Eliminar(int id);
        void Modificar(int id, Presupuestos p);
    }
}