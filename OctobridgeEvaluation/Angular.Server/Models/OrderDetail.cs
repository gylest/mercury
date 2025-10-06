using System;

namespace AngularClient.Models
{
    public class OrderDetail
    {
        public int LineId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime? RecordCreated { get; set; }
        public DateTime? RecordModified { get; set; }

        public OrderDetail()
        {
        }
    }
}
