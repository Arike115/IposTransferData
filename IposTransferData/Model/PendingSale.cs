using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class PendingSale
    {
        public string CustomerDetail { get; set; }
        public Guid? Customer_Id { get; set; }
        public Guid? ParentPendingSale_Id { get; set; }
        public Guid? Sale_Id { get; set; }
        public decimal NetCost { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Discount { get; set; }
        public decimal NetItemDiscount { get; set; }
        public string Title { get; set; }
        public int ApprovalCount { get; set; }
        public decimal ExtraCharges { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
        public string StoreDetail { get; set; }
        public int Status { get; set; }
        public string RefNo { get; set; }
        public string CurrencyCode { get; set; }
        public Guid? InvoiceId { get; set; }
    }
}
