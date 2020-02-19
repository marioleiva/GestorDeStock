using System;
using System.Collections.Generic;
using System.Text;
using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;
using System.Linq;

namespace SlnCibertec.Core.Services
{
    public class ProductService : IProductService
    {
        // campo para utilizar el contexto
        private readonly ICibertecContext _cibertecContext;

        public ProductService(ICibertecContext cibertecContext)
        {
            _cibertecContext = cibertecContext;
        }

        public int AgregarCategoriasAProducto(IList<Category> categorias, int idProducto)
        {
            // validaciones
            if (categorias.Count <= 0)
            {
                return 0;
            }

            // validar que el id de producto exista en BD
            var producto = _cibertecContext.Products.FirstOrDefault(p => p.ProductId == idProducto);

            if (producto == null)
            {
                return 0;
            }

            if (producto.ProductCategories == null)
            {
                producto.ProductCategories = new List<ProductCategory>();
            }

            // agregar las categorias
            foreach (var categoria in categorias)
            {
                var newProductCategory = new ProductCategory { CategoryId = categoria.CategoryId, ProductId = idProducto };
                producto.ProductCategories.Add(newProductCategory);
            }

            _cibertecContext.Commit();

            return categorias.Count;
        }

        public List<Product> ObtenerPrimeros2Productos()
        {
            return _cibertecContext.Products.Take(2).ToList();
        }

        public bool RegistrarProducto(Product nuevoProducto)
        {
            // validaciones
            if (string.IsNullOrEmpty(nuevoProducto.ProductName))
            {
                return false;
            }

            // obtener todos los productos registrados que posean el mismo nombre que el que se quiere registrar
            // query syntax
            //var productsWithSameName = from product in _cibertecContext.Products
            //                           where product.ProductName == nuevoProducto.ProductName
            //                           select product;

            // fluent syntax 
            var productsWithSameName = _cibertecContext.Products.Where(p => p.ProductName.ToUpper() == nuevoProducto.ProductName.ToUpper());

            if (productsWithSameName.Count() > 0)
            {
                // significa que existen productos registrados que tienen el mismo nombre que el que se desea registrar
                return false;
            }


            // agregar el proucto a BD
            _cibertecContext.Products.Add(nuevoProducto);

            return _cibertecContext.Commit() > 0;
        }
    }
}
