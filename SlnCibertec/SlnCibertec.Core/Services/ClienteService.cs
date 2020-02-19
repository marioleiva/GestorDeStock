using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnCibertec.Core.Services
{
    public class ClienteService : IClienteService
    {
        // campo para utilizar el contexto
        private readonly ICibertecContext _cibertecContext;
        public ClienteService(ICibertecContext cibertecContext)
        {
            _cibertecContext = cibertecContext;
        }
        public bool RegistrarCliente(Cliente nuevoCliente)
        {
            // validaciones
            if (string.IsNullOrEmpty(nuevoCliente.Nombre) || (string.IsNullOrEmpty(nuevoCliente.Apellido)) || (string.IsNullOrEmpty(nuevoCliente.NroDocumento)) )
            {
                return false;
            }

            var ClienteConDNIRepetido = _cibertecContext.Clientes.Where(p => p.NroDocumento.ToUpper() == nuevoCliente.NroDocumento.ToUpper());
            if (ClienteConDNIRepetido.Count() > 0)
            {
                return false;
            }
            // agregar el proucto a BD
            _cibertecContext.Clientes.Add(nuevoCliente);

            return _cibertecContext.Commit() > 0;
        }
    }
}
