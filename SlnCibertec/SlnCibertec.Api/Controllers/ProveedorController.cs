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
    [AllowAnonymous]
    public class ProveedorController : ControllerBase
    {
        private readonly ICibertecContext _context;
        private readonly IProveedorService _proveedorService;

        public ProveedorController(ICibertecContext context, IProveedorService proveedorService)
        {
            _context = context;
            _proveedorService = proveedorService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult ObtenerTodos()
        {
            return Ok(_context.Proveedores);
        }

        [HttpGet("{Id}")]
        public ActionResult ObtenerPorId(int Id)
        {
            return Ok(_context.Proveedores.Find(Id));
        }

        [HttpPost]

        public ActionResult Insertar(Proveedor request)
        {
            var registroCorrecto = _proveedorService.RegistrarProveedor(request);
            if (!registroCorrecto)
            {
                return BadRequest("Ocurrió un error con la solicitud enviada");
            }
            return Ok("se registró el proveedor satisfactoriamente");
        }

        [HttpPut]
        public ActionResult Actualizar(Proveedor proveedor)
        {
            Proveedor proveedorExistente = null;
            try
            {
                // obtener el producto de BD
                proveedorExistente = _context.Proveedores.Find(proveedor.Id);
            }
            catch (System.Exception)
            {
                return BadRequest("No se pudo obtener la información del proveedor existente");
            }
            // si no existe, devolver un error
            if (proveedorExistente == null)
            {
                return BadRequest("El proveedor no existe");
            }

            proveedorExistente.Empresa = proveedor.Empresa == null ? proveedorExistente.Empresa : proveedor.Empresa;
            proveedorExistente.Representante = proveedor.Representante;
            proveedorExistente.NroDocumento = proveedor.NroDocumento;
            proveedorExistente.Celular = proveedor.Celular;
            proveedorExistente.Direccion = proveedor.Direccion;
            proveedorExistente.Distrito = proveedor.Distrito;
            proveedorExistente.Provincia = proveedor.Provincia;
            proveedorExistente.Departamento = proveedor.Departamento;

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
            var Eliminar = _context.Proveedores.Find(id);
            if (Eliminar == null)
            {
                return BadRequest("El proveedor no existe");
            }

            _context.Proveedores.Remove(Eliminar);

            return Ok(_context.Commit());
        }
    }
}