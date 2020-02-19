using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnCibertec.Core.Services
{
    public class ProductoService : IProductoService
    {
        // campo para utilizar el contexto
        private readonly ICibertecContext _cibertecContext;
        public ProductoService(ICibertecContext cibertecContext)
        {
            _cibertecContext = cibertecContext;
        }
        
        public List<Producto> ObtenerPrimeros2Productos()
        {
            return _cibertecContext.Productos.Take(2).ToList();
        }

        public bool RegistrarProducto(Producto nuevoProducto)
        {
            // validaciones
            if (string.IsNullOrEmpty(nuevoProducto.Nombre))
            {
                return false;
            }

            var productsWithSameName = _cibertecContext.Productos.Where(p => p.Nombre.ToUpper() == nuevoProducto.Nombre.ToUpper());
            if (productsWithSameName.Count() > 0)
            {
                // significa que existen productos registrados que tienen el mismo nombre que el que se desea registrar
                return false;
            }
            // agregar el proucto a BD
            _cibertecContext.Productos.Add(nuevoProducto);

            return _cibertecContext.Commit() > 0;
        }
    }
}
