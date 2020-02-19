using SlnCibertec.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Interfaces
{
    public interface IProductoService
    {
        List<Producto> ObtenerPrimeros2Productos();
    
        bool RegistrarProducto(Producto nuevoProducto);

    }
}
