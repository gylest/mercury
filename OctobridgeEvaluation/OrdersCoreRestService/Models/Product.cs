using System;
using System.Collections.Generic;

#nullable disable

namespace OctobridgeCoreRestService.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public int ProductCategoryId { get; set; }
        public decimal Cost { get; set; }
        public DateTime RecordCreated { get; set; }
        public DateTime RecordModified { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
