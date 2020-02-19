using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;
using SlnCibertec.Core.Services;
using SlnCibertec.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SlnCibertec.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private readonly ICibertecContext _cibertecContext;
        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            var options = new DbContextOptionsBuilder<CibertecContext>()
                .UseInMemoryDatabase(databaseName: "cibertec_test")
                .Options;

            // instanciar el contexto
            _cibertecContext = new CibertecContext(options);

            // instranciar el servicio
            _productService = new ProductService(_cibertecContext);
        }

        [TestMethod]
        public void Registro_de_nombre_vacio_debe_retornar_falso()
        {
            // 1. Preparar la prueba
            var nuevoProducto = new Product { ProductName = string.Empty };

            // 2. Ejecutar el método a probar
            var resultado = _productService.RegistrarProducto(nuevoProducto);

            // 3. Hacer la comprobación
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Registro_de_nombre_nulo_debe_retornar_falso()
        {
            // 1. Preparar la prueba
            var nuevoProducto = new Product { ProductName = null };

            // 2. Ejecutar el método a probar
            var resultado = _productService.RegistrarProducto(nuevoProducto);

            // 3. Hacer la comprobación
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Registro_de_nombre_existente_debe_retornar_falso()
        {
            // 1. Preparar la prueba

            _cibertecContext.Products.Add(new Product { ProductName = "Repetido" });
            _cibertecContext.Commit();

            var nuevoProducto = new Product { ProductName = "repetido" };

            // 2. Ejecutar el método a probar
            var resultado = _productService.RegistrarProducto(nuevoProducto);

            // 3. Hacer la comprobación
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Registro_de_producto_valido_debe_retornar_verdadero()
        {
            // 1. Preparar la prueba
            var nuevoProducto = new Product { ProductName = "nuevo producto" };

            // 2. Ejecutar el método a probar
            var resultado = _productService.RegistrarProducto(nuevoProducto);

            // 3. Hacer la comprobación
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void Agregar_Categorias_A_Producto_Debe_Funcionar()
        {
            // 1. Preparar la prueba
            var productoNuevo = new Product { ProductName = "Producto con Categorías" };
            var categoria1 = new Category { CategoryName = "Categoría 1" };
            var categoria2 = new Category { CategoryName = "Categoría 2" };

            _cibertecContext.Products.Add(productoNuevo);
            _cibertecContext.Categories.Add(categoria1);
            _cibertecContext.Categories.Add(categoria2);

            _cibertecContext.Commit();

            // 2. Ejecutar la prueba
            var resultado = _productService.AgregarCategoriasAProducto(new List<Category> { categoria1, categoria2 }, productoNuevo.ProductId);

            // 3. Hacer la comprobación
            Assert.AreEqual(2, resultado);
        }
    }
}
