using IposTransferData.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class SaleItem
    {
        public Guid? PendingSale_Id { get; set; }
        public Guid? Sale_Id { get; set; }
        public Sale Sale { get; set; }
        public Guid? Item_Id { get; set; }
        public Item Item { get; set; }
        public Guid? InvoiceSale_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public double Quantity { get; set; }
        public double RecallQuantity { get; set; }
        public double TotalTransactionQuantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Price { get; set; }
        public double PreviousQuantity { get; set; }
        public Guid? ActiveItemBatch_Id { get; set; }
        public bool WasDiscounted { get; set; }
        public decimal Discount { get; set; }
        public decimal ExtraCharge { get; set; }
        public string Remarks { get; set; }
        public string CurrencyCode { get; set; }
        public SalesStatus SalesStatus { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
