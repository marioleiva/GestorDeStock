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
    public class ClientesController : ControllerBase
    {
        private readonly ICibertecContext _context;
        private readonly IClienteService _clienteService;

        public ClientesController(ICibertecContext context, IClienteService clienteService)
        {
            _context = context;
            _clienteService = clienteService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult ObtenerTodos()
        {
            return Ok(_context.Clientes);
        }

        [HttpGet("{Id}")]
        public ActionResult ObtenerPorId(int Id)
        {
            return Ok(_context.Clientes.Find(Id));
        }

        [HttpPost]

        public ActionResult Insertar(Cliente request)
        {
            var registroCorrecto = _clienteService.RegistrarCliente(request);
            if (!registroCorrecto)
            {
                return BadRequest("Ocurrió un error con la solicitud enviada");
            }
            return Ok("se registró el cliente satisfactoriamente");
        }

        [HttpPut]
        public ActionResult Actualizar(Cliente cliente)
        {
            Cliente clienteExistente = null;
            try
            {
                // obtener el producto de BD
                clienteExistente = _context.Clientes.Find(cliente.Id);
            }
            catch (System.Exception)
            {
                return BadRequest("No se pudo obtener la información del cliente existente");
            }
            // si no existe, devolver un error
            if (clienteExistente == null)
            {
                return BadRequest("El cliente no existe");
            }

            clienteExistente.Nombre = cliente.Nombre == null ? clienteExistente.Nombre : cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;
            clienteExistente.NroDocumento = cliente.NroDocumento;
            clienteExistente.Telefono = cliente.Telefono;
            clienteExistente.Direccion = cliente.Direccion;
            clienteExistente.Distrito = cliente.Distrito;
            clienteExistente.Provincia = cliente.Provincia;
            clienteExistente.Departamento = cliente.Departamento;

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
            var clienteAEliminar = _context.Clientes.Find(id);
            if (clienteAEliminar == null)
            {
                return BadRequest("El producto no existe");
            }

            _context.Clientes.Remove(clienteAEliminar);

            return Ok(_context.Commit());
        }
    }
}