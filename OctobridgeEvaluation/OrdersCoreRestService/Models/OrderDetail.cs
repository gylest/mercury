using System;
using System.Collections.Generic;

#nullable disable

namespace OctobridgeCoreRestService.Models
{
    public partial class OrderDetail
    {
        public int LineId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime RecordCreated { get; set; }
        public DateTime RecordModified { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
