using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_NahuelCondori99;
using tl2_tp8_2025_NahuelCondori99.ViewModels;

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

    //Crear presupuesto (GET)
    public IActionResult Create()
    {
        return View(new PresupuestoViewModel());
    }

    [HttpPost]
    public IActionResult Create(PresupuestoViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        var nuevo = new Presupuestos
        {
          NombreDestinatario = vm.NombreDestinatario,
          FechaCreacion = DateTime.Now  
        };

        repo.crearPresupuesto(nuevo);
        return RedirectToAction("Index");
    }

    //Detalles
    public IActionResult Details(int id)
    {
        var presupuesto = repo.GetById(id);
        if (presupuesto == null)
        {
            return NotFound();
        }
        return View(presupuesto);
    }

    //ELIMINAR 
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

    //EDIT
    public IActionResult Edit(int id)
    {
        var p = repo.GetById(id);
        if (p == null)
        {
            return NotFound();
        }
        var vm = new PresupuestoViewModel
        {
            IdPresupuesto = p.IdPresupuesto,
            NombreDestinatario = p.NombreDestinatario
        };
        return View(vm);
    }

    [HttpPost]
    public IActionResult Edit(PresupuestoViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var original = repo.GetById(vm.IdPresupuesto);

        var actualizado = new Presupuestos
        (
            vm.IdPresupuesto,
            vm.NombreDestinatario,
            original.FechaCreacion,
            original.Detalles
        );
        repo.Modificar(vm.IdPresupuesto, actualizado);
        
        return RedirectToAction("Index");
    }

    //AGREGAR PRODUCTOS AL PRESUPUESTO
    public IActionResult AddProduct(int id)
    {
        var prodRepo = new ProductoRepository();
        var productos = prodRepo.GetAll();

        var vm = new AgregarProductoViewModel
        {
          IdPresupuesto = id,
          ProductosDisponibles = productos  
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult AddProduct(AgregarProductoViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            var repoProd = new ProductoRepository();
            vm.ProductosDisponibles = repoProd.GetAll();
            return View(vm);
        }

        var presupuesto = repo.GetById(vm.IdPresupuesto);
        bool yaExiste = presupuesto.Detalles
                            .Any(d => d.Producto.IdProducto == vm.IdProducto);
        if (yaExiste)
        {
            ModelState.AddModelError("", "Este producto ya fue agregado al presupuesto");
            var repoProd = new ProductoRepository();
            vm.ProductosDisponibles = repoProd.GetAll();
            return View(vm);
        }
        repo.AgregarProducto(vm.IdPresupuesto, vm.IdProducto, vm.Cantidad);

        return RedirectToAction("Details", new {id = vm.IdPresupuesto});
    }
    //ESTO ES DEL TP8
    //Detalles de los presupuestos

    /*public IActionResult Details(int id)
    {
        var presupuesto = repo.GetById(id);
        if (presupuesto == null)
        {
            return NotFound();
        }
        return View(presupuesto);
    }*/

    //Agregar productos

    /*public IActionResult AddProduct(int id)
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
    
    //Eliminar productos del presupuesto
    public IActionResult DeleteProd(int idPresupuesto, int idProducto)
    {
        repo.EliminarProducto(idPresupuesto, idProducto);
        return RedirectToAction("Details", new{id = idPresupuesto});
    }

    //Modificar la cantidad

    [HttpGet]
    public IActionResult EditProduct(int idPresupuesto, int idProducto)//Mostrar formulario
    {
        var presupuesto  = repo.GetById(idPresupuesto);

        var detalle = presupuesto.Detalles.FirstOrDefault(d => d.Producto.IdProducto == idProducto);

        if (detalle == null)
        {
            return NotFound();
        }
        ViewBag.IdPresupuesto = idPresupuesto;
        ViewBag.IdProducto = idProducto;
        return View(detalle);
    }

    [HttpPost]
    public IActionResult EditProduct(int idPresupuesto, int idProducto, int cantidad)
    {
        repo.ModificarCantidad(idPresupuesto, idProducto, cantidad);

        return RedirectToAction("Details", new{id = idPresupuesto});
    }*/
}