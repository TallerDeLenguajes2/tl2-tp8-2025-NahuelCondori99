using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using tl2_tp8_2025_NahuelCondori99.ViewModels;
public class ProductosController : Controller
{
    private readonly ProductoRepository repo;
    public ProductosController()
    {
        repo = new ProductoRepository();
    }

    //Listar los productos
    public IActionResult Index()
    {
        var lista = repo.GetAll();
        return View(lista);
    }

    //GET: Create

    public IActionResult Create()
    {
        return View(new ProductoViewModel());
    }

    //POST: Create
    [HttpPost]
    public IActionResult Create(ProductoViewModel productoVM)
    {
        if (!ModelState.IsValid)
        {
            return View(productoVM);
        }
        var producto = new Productos
        {
            Descripcion = productoVM.Descripcion,
            Precio = productoVM.Precio
        };

        repo.Alta(producto);

        return RedirectToAction("Index");
    }

    //GET: Edit

    public IActionResult Edit(int id)
    {
        var p = repo.GetById(id);
        if (p == null)
        {
            return NotFound();
        }
        var vm = new ProductoViewModel
        {
            IdProducto = p.IdProducto,
            Descripcion = p.Descripcion,
            Precio = p.Precio
        };
        return View(vm);
    }

    //POST: Edit

    [HttpPost]
    public IActionResult Edit(ProductoViewModel productoVM)
    {
        if(!ModelState.IsValid)
        {
            return View(productoVM);
        }
        var p = new Productos
        {
          IdProducto = productoVM.IdProducto,
          Descripcion = productoVM.Descripcion,
          Precio = productoVM.Precio  
        };

        repo.Modificar(productoVM.IdProducto, p);
        return RedirectToAction("Index");
    }

    //GET: Delete
    public IActionResult Delete(int id)
    {
        var p = repo.GetById(id);

        if (p == null)
        {
            return NotFound();
        }
        return View(p);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        repo.Eliminar(id);
        return RedirectToAction("Index");
    }

    //Edit
    public IActionResult Details(int id)
    {
        var p = repo.GetById(id);

        if (p == null)
        {
            return NotFound();
        }

        return View(p);
    }
}