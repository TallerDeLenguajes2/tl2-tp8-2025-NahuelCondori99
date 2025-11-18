using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

public class ProductosController : Controller
{
    private ProductoRepository productoRepository;
    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }

    //Listar los productos
    [HttpGet]
    public IActionResult Index()
    {
        List<Productos> listaProd = productoRepository.GetAll();
        return View(listaProd);
    }

    //Detalles de los productos
    [HttpGet]
    public IActionResult Details(int id)
    {
        var producto = productoRepository.GetById(id);
        if (producto == null)
        {
            return NotFound();
        }
        return View(producto);
    }
}