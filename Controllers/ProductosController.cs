using Microsoft.AspNetCore.Mvc;
using tp.Models;
using System.Collections.Generic;
using System.Linq;
using tp.Repositorios;

namespace tp.Controllers;

[ApiController]
[Route("[controller]")]

public class ProductoController : ControllerBase
{
    private ProductoRepository productoRepository;

    public ProductoController()
    {
        productoRepository = new ProductoRepository();
    }

    [HttpGet]
    // [Route("/api/Producto")]
    public ActionResult<List<Productos>> ObtenerProductos()
    {
        List<Productos> productos = productoRepository.GetProductos();
        return Ok(productos);
    }


    [HttpPut("{id}")]
    public ActionResult Modificar(int id, [FromBody] Productos producto)
    {
        var productos = productoRepository.GetProductos();
        var productoExistente = productos.FirstOrDefault(p => p.IdProducto == id);

        if (productoExistente == null)
        {
            return NotFound("El producto no existe.");
        }

        productoRepository.Update(id, producto);
        return Ok("Producto actualizado exitosamente.");
    }

    [HttpPost]
    public ActionResult Agregar([FromBody] Productos producto)
    {
        productoRepository.Create(producto);
        return Ok("Producto agregado exitosamente.");
    }

    [HttpGet("{id}")]
    public ActionResult<Productos> ObtenerPorId(int id)
    {
        var producto = productoRepository.GetByID(id);
        if (producto == null)
        {
            return NotFound("El producto no existe.");
        }
        return Ok(producto);
    }

    [HttpDelete("{id}")]
    public ActionResult Eliminar(int id)
    {
        var productoExistente = productoRepository.GetByID(id);
        if (productoExistente == null)
        {
            return NotFound("El producto no existe.");
        }

        productoRepository.Delete(id);
        return Ok("Producto eliminado exitosamente.");
    }
}
