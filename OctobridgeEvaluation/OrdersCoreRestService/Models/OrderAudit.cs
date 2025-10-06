using System;
using System.Collections.Generic;

#nullable disable

namespace OctobridgeCoreRestService.Models
{
    public partial class OrderAudit
    {
        public int AuditId { get; set; }
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
        public string AuditType { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
