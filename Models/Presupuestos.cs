public class Presupuestos
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestoDetalles> detalles;

    public Presupuestos(int idPresupuesto, string nombreDestinatario, DateTime fechaCreacion, List<PresupuestoDetalles> detalles)
    {
        this.IdPresupuesto = idPresupuesto;
        this.NombreDestinatario = nombreDestinatario;
        this.FechaCreacion = fechaCreacion;
        this.Detalles = detalles;
    }

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestoDetalles> Detalles { get => detalles; set => detalles = value; }

    public double MontoPresupuesto()
    {
        double total = 0;

        foreach (var prod in detalles)
        {
            total += prod.Producto.Precio * prod.Cantidad;
        }
        return total;
    }

    public double MontoPresupuestoIVA()
    {
        double montoBase = MontoPresupuesto();
        double iva = montoBase * 0.21;

        return (montoBase * iva);

    }

    public int CantidadProductos()
    {
        int totalCantidad = 0;

        foreach (var p in detalles)
        {
            totalCantidad += p.Cantidad;
        }
        return totalCantidad;
    }
}