using System;
using System.Collections.Generic;

#nullable disable

namespace TestDatabaseNUnit.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public int CustomerId { get; set; }
        public decimal FreightAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalDue { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime RecordCreated { get; set; }
        public DateTime RecordModified { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
