using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class ItemBatch : BaseEntity
    {
        public bool IsActive { get; set; }
        public Guid Item_Id { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset? BestBefore { get; set; }
        public DateTimeOffset? MadeOn { get; set; }
        public double Quantity { get; set; }
        public StockupSources StockupSource { get; set; }
        public double PreviousQuantity { get; set; }
        public DateTimeOffset? PreviouStockDate { get; set; }
        public Guid? PreviousItemBatch_Id { get; set; }
        public Guid? PurchaseOrder_Id { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
        public ItemBatchStatuses BatchStatus { get; set; }
        public double ReorderLevel { get; set; }
        public decimal CostPrice { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal Discount { get; set; }

    }

    public enum ItemBatchStatuses
    {
        PENDING = 0,
        ACTIVE = 1,
        CLOSED = 2
    }

    public enum StockupSources
    {
        UNKNOWN,
        PURCHASE_ORDER,
        DIRECT_UPDATE
    }
}
