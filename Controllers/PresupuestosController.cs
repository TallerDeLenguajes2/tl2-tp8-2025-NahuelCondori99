using Microsoft.AspNetCore.Mvc;

public class PresupuestosController : Controller
{
    private readonly PresupuestosRepository repo;
    public PresupuestosController()
    {
        repo = new PresupuestosRepository();
    }   

    //Listar presupuestos
    public IActionResult Index()
    {
        var lista = repo.GetAll();
        return View(lista);
    }

    //Detalles de los presupuestos

    public IActionResult Details(int id)
    {
        var presupuesto = repo.GetById(id);
        if (presupuesto == null)
        {
            return NotFound();
        }
        return View(presupuesto);
    }

    //Agregar productos

    public IActionResult AddProduct(int id)
    {
        var prodRepo = new ProductoRepository();
        var listaprod = prodRepo.GetAll();

        ViewBag.IdPresupuesto = id;
        return View(listaprod);
    }

    [HttpPost]
    public IActionResult AddProduct(int idPresupuesto, int idProducto, int cantidad)
    {
        repo.AgregarProducto(idPresupuesto, idProducto, cantidad);
        return RedirectToAction("Details", new{id = idPresupuesto});
    }
}