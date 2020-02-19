using System.Collections.Generic;

namespace SlnCibertec.Core.Entities
{
    public class Product
    {        
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityPerUnit { get; set; }
        public int UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
        // esta propiedad indica la relación de 1 a muchos con Category
        //public int CategoryId { get; set; }        
        //public Category Category { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }

    public class ProductCategory
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
