using Microsoft.EntityFrameworkCore;
using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;

namespace SlnCibertec.Infra.Data
{
    public class CibertecContext : DbContext, ICibertecContext
    {
        public CibertecContext(DbContextOptions<CibertecContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configuración del modelo con Fluent API
            //modelBuilder.Entity<Product>()
            //    .Property(p => p.ProductName) // para la columna product name
            //    .HasMaxLength(300)
            //    .IsRequired() // que sea not null
            //    .HasColumnType("varchar(300)") // el tipo de datos en BD
            //    .HasColumnName("p_name"); // personalizar el nombre de la columna


            // configurar una relación de muchos a muchos
            modelBuilder.Entity<ProductCategory>().HasKey(pc => new { pc.ProductId, pc.CategoryId });

            // configurar la precisión para el campo UnitPrice de Product
            modelBuilder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(10,2)");

            // product name sea requerido
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductName)
                .IsRequired();
        }

        public int Commit()
        {
            return SaveChanges();
        }

        public bool Migrate()
        {
            try
            {
                Database.Migrate();
                return true;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        // agregar los db set que serán las tablas que se crearán en la BD
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        //public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Compra> Compras { get; set; }

    }
}

