using IposTransferData.Enum;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Dto
{
   public class ProductDto
    {
        public int ProductId { get; set; }
        public Guid ProductUId { get; set; }
        public byte[] RowVersion { get; set; }
        public string Barcode { get; set; }
        public double Quantity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string KeyFeatures { get; set; }
        public string Specification { get; set; }
        public string Brand { get; set; }
        public double Weight { get; set; }
        public ItemTypes ItemsType { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal SellingCost { get; set; }
        public decimal PreviousSellingCost { get; set; }
        public decimal ActualCost { get; set; }
        public bool IsDiscountable { get; set; }
        public decimal DiscountLimit { get; set; }
        public bool IsDiscontinue { get; set; }
        public string ItemCode { get; set; }
        public string LogoUrl { get; set; }
        public string LogoName { get; set; }
        public long LogoFileSize { get; set; }
        public string LogoOriginalFileName { get; set; }
        public decimal ExtraCharge { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
        public string? TitleSlug { get; set; }
        public bool IsFavourite { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
