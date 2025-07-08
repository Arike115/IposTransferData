using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class PurchaseOrder : BaseEntity
    {
        public Guid? Supplier_Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public DateTimeOffset? DeliveredDate { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public string RefNo { get; set; }
        public string DeliveryAddressLine1 { get; set; }
        public string DeliveryAddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
    }

    public enum PurchaseOrderStatus
    {
        PENDING,
        CANCELLED,
        DELIVERED,
        DRAFT
    }
}
