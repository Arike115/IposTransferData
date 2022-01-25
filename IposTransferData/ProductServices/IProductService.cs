using IposTransferData.Dto;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IposTransferData.ProductServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<IEnumerable<ProductDto>> GetProductsByCategoryId(int category_UId);
        Task InsertProductData(Guid Id, string Barcode, double Quantity, string Title, string Description, decimal SellingPrice, decimal CostPrice, string LogoUrl, string LogoOriginalFileName,
                           long LogoFileSize, bool IsDiscountable, bool IsDiscontinue, decimal ReorderLevel, DateTime? ModifiedOn, DateTime? CreatedOn, bool IsDeleted, float? Weight, int? ItemsType, 
                           decimal PreviousSellingCost, decimal? ExtraCharge, decimal? DiscountLimit);
    }
}