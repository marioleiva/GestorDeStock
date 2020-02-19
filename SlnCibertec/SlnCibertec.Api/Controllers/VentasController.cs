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
    public class VentasController : ControllerBase
    {
        private readonly ICibertecContext _context;
        private readonly INuevaVentaService _nuevaVentaService;

        public VentasController(ICibertecContext context, INuevaVentaService ventaService)
        {
            _context = context;
            _nuevaVentaService = ventaService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult ObtenerTodos()
        {
            return Ok(_context.Ventas);
        }

        [HttpGet("{Id}")]
        public ActionResult ObtenerPorId(int Id)
        {
            return Ok(_context.Ventas.Find(Id));
        }

        [HttpPost]

        public ActionResult Insertar(Venta request)
        {

            _context.Ventas.Add(request);
            var result = _context.Commit();
            if (result > 0)
            {
                return Ok(request.Id);
            }

            return Ok("No se pudo insertar el registro");
            //var registroCorrecto = _nuevaVentaService.RegistrarNuevaVenta(request);
            //if (registroCorrecto==0)
            //{
            //    return BadRequest(0);
            //}
            //return Ok(registroCorrecto);
        }

        [HttpPut]
        public ActionResult Actualizar(DetalleVenta detalleVenta)
        {
            Venta Existente = null;
            try
            {
                _context.DetalleVentas.Add(detalleVenta);
                var result = _context.Commit();
                if (result > 0)
                {
                    
                    try
                    {
                        //return Ok(detalleVenta.Id);
                        Existente = _context.Ventas.Find(detalleVenta.VentaId);
                        Existente.Total = detalleVenta.SubTotal;
                        
                        var resultado = _context.Commit();

                        return Ok(resultado);
                    }
                    catch (System.Exception ex)
                    {
                        // log del error (ex)
                        return BadRequest("Ocurrió un error al tratar de grabar en BD");
                    }
                }
                return BadRequest("Ocurrió un error al tratar de grabar en BD");
            }
            catch (System.Exception)
            {
                return BadRequest("No se pudo obtener la información del cliente existente");
            }
        }

    }
}