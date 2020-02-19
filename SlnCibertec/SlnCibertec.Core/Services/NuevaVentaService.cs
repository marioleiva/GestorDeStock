using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;
using SlnCibertec.Core.Requests;
using SlnCibertec.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Services
{
    public class NuevaVentaService : INuevaVentaService
    {
        private readonly ICibertecContext _cibertecContext;
        public NuevaVentaService(ICibertecContext cibertecContext)
        {
            _cibertecContext = cibertecContext;
        }

        public int RegistrarNuevaVenta(Venta nuevaVenta)
        {
            // validaciones
            //if (string.IsNullOrEmpty(nuevaVenta.Users))
            //{
            //    return 0;
            //}

            // agregar el proucto a BD
            _cibertecContext.Ventas.Add(nuevaVenta);
            _cibertecContext.Commit();
            return nuevaVenta.Id;
            
        }

    }
}
