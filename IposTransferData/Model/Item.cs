using IposTransferData.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Item
    {
        public Guid? Id { get; set; }
        public string Barcode { get; set; }
        public double Quantity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float? Weight { get; set; }
        public int? ItemsType { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal SellingCost { get; set; }
        public decimal PreviousSellingCost { get; set; }
        public decimal ActualCost { get; set; }
        public bool IsDiscountable { get; set; }
        public bool IsDiscontinue { get; set; }
        public decimal DiscountLimit { get; set; }
        public string ItemCode { get; set; }
        public string LogoUrl { get; set; }
        public string LogoName { get; set; }
        public string LogoContentType { get; set; }
        public long LogoFileSize { get; set; }
        public string LogoOriginalFileName { get; set; }
        public decimal ExtraCharge { get; set; }
        public string Logo { get; set; }
        public string ManufacturingDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }


        public static explicit operator Item(ProductDto source)
        {
            var destination = new Item();
            destination.Id = new Guid();
            destination.Barcode = source.Barcode;
            destination.Quantity = source.Quantity;
            destination.Title = source.Name;
            destination.Description = source.Description;
            destination.SellingCost = source.Price;
            destination.ActualCost = source.CostPrice;
            destination.LogoUrl = source.PhotoUrl;
            destination.LogoOriginalFileName = source.FileName;
            destination.LogoFileSize = source.FileSize;
            destination.IsDiscountable = source.IsDiscountable;
            destination.IsDiscontinue = source.IsDiscountinued;
            destination.ReorderLevel = source.ReorderLevel;
            destination.CreatedOn = source.EntryDate;
            destination.ModifiedOn = source.EntryDate;
            destination.IsDeleted = source.IsDeleted;
            return destination;
        }

       
    }

}
