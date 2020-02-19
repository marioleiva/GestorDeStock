using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Requests;
using SlnCibertec.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Interfaces
{
    public interface INuevaVentaService
    {
        int RegistrarNuevaVenta(Venta nuevaVenta);
    }
}
