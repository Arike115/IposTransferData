using IposTransferData.Dto;
using IposTransferData.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Item : BaseEntity
    { 
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

        public static explicit operator Item(ProductDto source)
        {
            var destination = new Item();
            destination.Id = source.ProductUId;
            destination.Barcode = source.Barcode;
            destination.Quantity = source.Quantity;
            destination.Title = source.Title;
            destination.Description = source.Description;
            destination.SellingCost = source.SellingCost;
            destination.ActualCost = source.ActualCost;
            destination.LogoUrl = source.LogoUrl;
            destination.LogoOriginalFileName = source.LogoOriginalFileName;
            destination.LogoFileSize = source.LogoFileSize;
            destination.IsDiscountable = source.IsDiscountable;
            destination.IsDiscontinue = source.IsDiscontinue;
            destination.ReorderLevel = source.ReorderLevel;
            destination.CreatedOn = source.CreatedOn;
            destination.ModifiedOn = source.ModifiedOn;
            destination.IsDeleted = source.IsDeleted;
            return destination;
        }

       
    }

}
