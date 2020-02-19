using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnCibertec.Core.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly ICibertecContext _cibertecContext;
        public ProveedorService(ICibertecContext cibertecContext)
        {
            _cibertecContext = cibertecContext;
        }
        public bool RegistrarProveedor(Proveedor nuevoProveedor)
        {
            // validaciones
            if (string.IsNullOrEmpty(nuevoProveedor.Empresa) || (string.IsNullOrEmpty(nuevoProveedor.NroDocumento)) || (string.IsNullOrEmpty(nuevoProveedor.Representante)))
            {
                return false;
            }

            var DNIRepetido = _cibertecContext.Proveedores.Where(p => p.NroDocumento.ToUpper() == nuevoProveedor.NroDocumento.ToUpper());
            if (DNIRepetido.Count() > 0)
            {
                return false;
            }
            // agregar el proucto a BD
            _cibertecContext.Proveedores.Add(nuevoProveedor);

            return _cibertecContext.Commit() > 0;
        }
    }
}
