using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;

namespace SlnCibertec.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[AllowAnonymous]
    public class ProductosController : ControllerBase
    {
        private readonly ICibertecContext _context;
        private readonly IProductoService _productoService;

        public ProductosController(ICibertecContext context, IProductoService productoService)
        {
            _context = context;
            _productoService = productoService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult ObtenerTodos()
        {
            return Ok(_context.Productos);
        }

        [HttpGet("{Id}")]
        public ActionResult ObtenerPorId(int Id)
        {
            return Ok(_context.Productos.Find(Id));
        }

        [HttpPost]
        
        public ActionResult Insertar(Producto request)
        {
            var registroCorrecto = _productoService.RegistrarProducto(request);
            if (!registroCorrecto)
            {
                return BadRequest("Ocurrió un error con la solicitud enviada");
            }
            return Ok("se registró el producto satisfactoriamente");
        }

        [HttpPut]
        public ActionResult Actualizar(Producto producto)
        {
            Producto productoExistente = null;
            try
            {
                // obtener el producto de BD
                productoExistente = _context.Productos.Find(producto.ProductoId);
            }
            catch (System.Exception)
            {
                return BadRequest("No se pudo obtener la información del producto existente");
            }
            // si no existe, devolver un error
            if (productoExistente == null)
            {
                return BadRequest("El producto no existe");
            }

            productoExistente.Nombre = producto.Nombre == null ? productoExistente.Nombre : producto.Nombre;
            //productoExistente.es = product.Discontinued;
            productoExistente.Cantidad = producto.Cantidad;
            productoExistente.Precio = producto.Precio;
            productoExistente.CantidadMaxima = producto.CantidadMaxima;
            productoExistente.CantidadMinima = producto.CantidadMinima;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Estado = producto.Estado;

            try
            {
                var resultado = _context.Commit();

                return Ok(resultado);
            }
            catch (System.Exception ex)
            {
                // log del error (ex)
                return BadRequest("Ocurrió un error al tratar de grabar en BD");
            }

        }

        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            // obtener la entidad que se quiere eliminar
            var productoAEliminar = _context.Productos.Find(id);
            if (productoAEliminar == null)
            {
                return BadRequest("El producto no existe");
            }

            _context.Productos.Remove(productoAEliminar);

            return Ok(_context.Commit());
        }
    }
}