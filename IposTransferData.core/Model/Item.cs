using IposTransferData.core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.core.Model
{
    public class Item
    {
        public string Barcode { get; set; }
        public double Quantity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string KeyFeatures { get; set; }
        public string Specification { get; set; }
        public string Brand { get; set; }
        public double Weight { get; set; }
        public int ItemsType { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PreviousSellingCost { get; set; }
        public decimal CostPrice { get; set; }
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
        public int LeadTime { get; set; }
        public string ExpiryDate { get; set; }
        public string ManufacturingDate { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }


        public static explicit operator Item(ProductDto source)
        {
            var destination = new Item();
            destination.Barcode = source.Barcode;
            destination.Quantity = source.Quantity;
            destination.Title = source.Name;
            destination.Description = source.Description;
            destination.SellingPrice = source.Price;
            destination.CostPrice = source.CostPrice;
            destination.LogoUrl = source.PhotoUrl;
            destination.LogoOriginalFileName = source.FileName;
            destination.LogoFileSize = source.FileSize;
            destination.IsDiscountable = source.IsDiscountable;
            destination.IsDiscontinue = source.IsDiscountinued;
            destination.ReorderLevel = source.ReorderLevel;
            destination.ExpiryDate = source.ExpiryDate.ToString();
            destination.CreatedOn = source.CreatedOn.ToString();
            destination.ModifiedOn = source.ModifiedOn.ToString();

            return destination;
        }
    }
}
