using System;
using SlnCibertec.Core.Entities;
using SlnCibertec.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace SlnCibertec.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            // crear las opciones para inicializar el contexto
            //var optionsBuilder = new DbContextOptionsBuilder<CibertecContext>();

            // indicar que nos vamos a conectar a una BD SQL server
            // también indicar la cadena de conexión
            //optionsBuilder.UseSqlServer("server=.;database=cibertec_bd;user id=web-developer;password=web-developer");
            //options.UseOracle();

            // crear el contexto e indicarle que cree la base de datos
            //using (var context = new CibertecContext(optionsBuilder.Options))
            //{
            //    // llamar al método EnsureCreated para asegurarnos que la BD está creada. Si no lo está, la va crear.
            //    //context.Database.EnsureCreated();

            //    // agregar data de prueba
            //    var categoria1 = new Category { CategoryName = "Categría 1", Description = "Descripción de la categoría 1", Picture = "https://via.placeholder.com/200" };
            //    var categoria2 = new Category { CategoryName = "Categría 2", Description = "Descripción de la categoría 2", Picture = "https://via.placeholder.com/200" };

            //    context.Categories.Add(categoria1);
            //    context.Categories.Add(categoria2);

            //    // agregar productos
            //    var producto1 = new Product { ProductName = "Producto 1" };
            //    var producto2 = new Product { ProductName = "Producto 2" };

            //    context.Products.Add(producto1);
            //    context.Products.Add(producto2);

            //    //context.SaveChanges();                    

            //    // agregar las relaciones                
            //    context.ProductCategories.Add(new ProductCategory { Product = producto1, Category = categoria1 });
            //    context.ProductCategories.Add(new ProductCategory { Product = producto2, Category = categoria1 });
            //    context.ProductCategories.Add(new ProductCategory { Product = producto2, Category = categoria2 });

            //    // guardar cambios
            //    context.SaveChanges();
            //}
        }
    }
}
