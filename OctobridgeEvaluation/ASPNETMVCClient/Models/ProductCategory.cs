using System;
using System.Collections.Generic;

#nullable disable

namespace MVCClient.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime RecordCreated { get; set; }
        public DateTime RecordModified { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
