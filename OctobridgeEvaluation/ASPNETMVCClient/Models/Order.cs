using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MVCClient.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }
        [Required]
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [Display(Name = "Freight Amount")]
        public decimal FreightAmount { get; set; }
        [Display(Name = "Sub Total")]
        public decimal SubTotal { get; set; }
        [Display(Name = "Total Due")]
        public decimal TotalDue { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime? PaymentDate { get; set; }
        [Display(Name = "Shipped Date")]
        public DateTime? ShippedDate { get; set; }
        [Display(Name = "Cancel Date")]
        public DateTime? CancelDate { get; set; }
        [Display(Name = "Record Created")]
        public DateTime RecordCreated { get; set; }
        [Display(Name = "Record Modified")]
        public DateTime RecordModified { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
