using System.Collections.Generic;

namespace SlnCibertec.Core.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}
