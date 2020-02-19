using System;
using System.Collections.Generic;
using System.Text;
using SlnCibertec.Core.Entities;

namespace SlnCibertec.Core.Interfaces
{
    public interface IProductService
    {
        List<Product> ObtenerPrimeros2Productos();
        /// <summary>
        /// Lo que hace el método
        /// </summary>
        /// <param name="nuevoProducto">El nuevo producto que se va a isnertar</param>
        /// <returns></returns>
        bool RegistrarProducto(Product nuevoProducto);

        /// <summary>
        /// Lo que hace el método
        /// </summary>
        /// <param name="categorias"></param>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        int AgregarCategoriasAProducto(IList<Category> categorias, int idProducto);
    }
}
