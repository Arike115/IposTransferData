using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.core.Dto
{
   public class ProductDto
    {
        public int ProductId { get; set; }
        public Guid? ProductUId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime EntryDate { get; set; }
        public Guid? Insert_UId { get; set; }
        public Guid? Update_UId { get; set; }
        public string PhotoUrl { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
        public bool IsDiscountable { get; set; }
        public string Barcode { get; set; }
        public string Notes { get; set; }
        public decimal CostPrice { get; set; }
        public int ReorderLevel { get; set; }
        public string ContentType { get; set; }
        public int FileSize { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool CanExpire { get; set; }
        public int Category_UId { get; set; }
        public bool IsDiscountinued { get; set; }
        public int CreatedOn { get; set; }
        public int ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string BatchNumber { get; set; }
    }
}
