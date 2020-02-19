using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlnCibertec.Core.Interfaces;

namespace SlnCibertec.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DetalleVentaController : ControllerBase
    {
        private readonly ICibertecContext _context;
        private readonly INuevaVentaService _nuevaVentaService;

        public DetalleVentaController(ICibertecContext context, INuevaVentaService ventaService)
        {
            _context = context;
            _nuevaVentaService = ventaService;
        }

        [HttpGet("{Id}")]
        public ActionResult ObtenerPorId(int Id)
        {
            return Ok(_context.DetalleVentas.Where(x => x.VentaId == Id));
        }

        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            // obtener la entidad que se quiere eliminar
            var Eliminar = _context.DetalleVentas.Find(id);
            if (Eliminar == null)
            {
                return BadRequest("El DetalleVentas no existe");
            }

            _context.DetalleVentas.Remove(Eliminar);

            return Ok(_context.Commit());
        }
    }
}