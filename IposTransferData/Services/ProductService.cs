using Dapper;
using IposTransferData.DataAccess;
using IposTransferData.Dto;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataDapperService _db;
        public ProductService(IDataDapperService db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryId(int category_UId)
        {
            var sql = @"select * From Product po
                        WHERE po.Category_UId = @category_UId";
            var Product = await _db.GetData<ProductDto>(sql, new { Category_UId = category_UId });
            return Product;

        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var sql = @"select * From Product p
                        WHERE p.IsDeleted <> 1
                        ORDER BY p.ModifiedOnUtc DESC";

            var product = await _db.GetData<ProductDto>(sql, null);
            return product;

        }


        
        public async Task InsertProductData( Guid Id,string Barcode, double Quantity, string Title, string Description, decimal SellingCost, decimal ActualCost,string LogoUrl, string LogoOriginalFileName,
                           long LogoFileSize, bool IsDiscountable, bool IsDiscontinue, decimal ReorderLevel,DateTime? ModifiedOn, DateTime? CreatedOn, bool IsDeleted, float? Weight, int? ItemsType,
                           decimal PreviousSellingCost, decimal? ExtraCharge, decimal? DiscountLimit)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", Id);
            parameter.Add("@Barcode", Barcode);
            parameter.Add("@Quantity", Quantity);
            parameter.Add("@Title", Title);
            parameter.Add("@Description", Description);
            parameter.Add("@SellingCost", SellingCost);
            parameter.Add("@ActualCost", ActualCost);
            parameter.Add("@LogoUrl", LogoUrl);
            parameter.Add("@LogoOriginalFileName", LogoOriginalFileName);
            parameter.Add("@LogoFileSize", LogoFileSize);
            parameter.Add("@IsDiscountable", IsDiscountable);
            parameter.Add("@IsDiscontinue", IsDiscontinue);
            parameter.Add("@ReorderLevel", ReorderLevel);
            parameter.Add("@IsDeleted", IsDeleted);
            parameter.Add("@ModifiedOn", ModifiedOn);
            parameter.Add("@CreatedOn", CreatedOn);
            parameter.Add("@Weight", Weight ?? 0);
            parameter.Add("@ItemsType", ItemsType ?? 0);
            parameter.Add("@PreviousSellingCost", PreviousSellingCost);
            parameter.Add("@ExtraCharge", ExtraCharge ?? 0);
            parameter.Add("@DiscountLimit", DiscountLimit ?? 0);

            //cmd.Parameters.Add(new SqlParameter("@TaxRate", string.IsNullOrEmpty(taxRateTxt.Text) ? (object)DBNull.Value : taxRateTxt.Text));
            var sql = @"INSERT into Item(Id,Barcode,Quantity,Title,Description,SellingCost,ActualCost,LogoUrl,LogoOriginalFileName,
                            LogoFileSize,IsDiscountable,IsDiscontinue,ReorderLevel,ModifiedOn,CreatedOn,IsDeleted,Weight,ItemsType,PreviousSellingCost,ExtraCharge,DiscountLimit) 
                        VALUES (@Id, @Barcode, @Quantity, @title, @Description, @SellingCost, @ActualCost,@LogoUrl,@LogoOriginalFileName,@LogoFileSize,
                        @IsDiscountable,@IsDiscontinue,@ReorderLevel,@ModifiedOn,@CreatedOn,@IsDeleted,@Weight,@ItemsType,@PreviousSellingCost,@ExtraCharge,@DiscountLimit)";
           


            await _db.SaveData(sql, parameter);
            //return item;
            //using (var result = _db.SaveData(sql, product))
            //{
            //    var items = result.Read<Item>().SingleOrDefault();
            //    return items;
            //    if (items != null)
            //    {
            //        items.AddRange(items);
            //    }
            //}
        }
    }
}
