using Microsoft.AspNetCore.Mvc;
using SlnCibertec.Infra.Data;
using System.Collections.Generic;
using SlnCibertec.Core.Entities;
using System.Linq;
using SlnCibertec.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SlnCibertec.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICibertecContext _context;
        private readonly IProductService _productService;

        public ProductsController(ICibertecContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult ObtenerTodos()
        {
            return Ok(_context.Products);
        }

        [HttpGet("{productId}")]
        public ActionResult ObtenerPorId(int productId)
        {
            return Ok(_context.Products.Find(productId));
        }

        [HttpPost]
        public ActionResult Insertar(Product request)
        {
            var registroCorrecto = _productService.RegistrarProducto(request);
            if (!registroCorrecto)
            {
                return BadRequest("Ocurrió un error con la solicitud enviada");
            }
            return Ok("se registró el producto satisfactoriamente");
        }

        [HttpPut]
        public ActionResult Actualizar(Product product)
        {
            Product productoExistente = null;
            try
            {
                // obtener el producto de BD
                productoExistente = _context.Products.Find(product.ProductId);
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

            productoExistente.ProductName = product.ProductName == null ? productoExistente.ProductName : product.ProductName;
            productoExistente.Discontinued = product.Discontinued;
            productoExistente.QuantityPerUnit = product.QuantityPerUnit;
            productoExistente.UnitPrice = product.UnitPrice;
            productoExistente.UnitsInStock = product.UnitsInStock;

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
            var productoAEliminar = _context.Products.Find(id);
            if (productoAEliminar == null)
            {
                return BadRequest("El producto no existe");
            }

            _context.Products.Remove(productoAEliminar);

            return Ok(_context.Commit());
        }
    }
}
