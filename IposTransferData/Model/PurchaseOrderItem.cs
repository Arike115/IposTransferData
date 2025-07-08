using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class PurchaseOrderItem : BaseEntity
    {
        public Guid PurchaseOrder_Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public Guid Item_Id { get; set; }
        public double CurrentQuantity { get; set; }
    }
}
