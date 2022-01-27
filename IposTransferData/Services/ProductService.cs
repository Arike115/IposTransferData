using Dapper;
using IposTransferData.Dto;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services
{
    public class ProductService : IProductService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;
        string connectionString = "Server=DESKTOP-M3U3Q02\\S_SQLEXPRESS;Database=Iposv3;Trusted_Connection=true;MultipleActiveResultSets=false;TrustServerCertificate=True";
        string desString = "Server=DESKTOP-M3U3Q02\\S_SQLEXPRESS;Database=IposDataRetrieved;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True";
        public ProductService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _destinationConnection = new SqlConnection(desString); 
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryId(int category_UId)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"select * From Product po
                        WHERE po.Category_UId = @category_UId";
            var Product =  await _sqlConnection.QueryAsync<ProductDto>(sql, new { Category_UId = category_UId });
            return Product;

        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();
            var sql = @"select * From Product p
                        WHERE p.IsDeleted <> 1
                        ORDER BY p.ModifiedOnUtc DESC";
            
            var product = await _sqlConnection.QueryAsync<ProductDto>(sql, null);
            return product;

        }


        
        public async Task InsertProductData( Guid Id,string Barcode, double Quantity, string Title, string Description, decimal SellingCost, decimal ActualCost,string LogoUrl, string LogoOriginalFileName,
                           long LogoFileSize, bool IsDiscountable, bool IsDiscontinue, decimal ReorderLevel,DateTime? ModifiedOn, DateTime? CreatedOn, bool IsDeleted, float? Weight, int? ItemsType,
                           decimal PreviousSellingCost, decimal? ExtraCharge, decimal? DiscountLimit)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

           
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

            var sql = @"INSERT into Item(Id,Barcode,Quantity,Title,Description,SellingCost,ActualCost,LogoUrl,LogoOriginalFileName,
                            LogoFileSize,IsDiscountable,IsDiscontinue,ReorderLevel,ModifiedOn,CreatedOn,IsDeleted,Weight,ItemsType,PreviousSellingCost,ExtraCharge,DiscountLimit) 
                        VALUES (@Id, @Barcode, @Quantity, @title, @Description, @SellingCost, @ActualCost,@LogoUrl,@LogoOriginalFileName,@LogoFileSize,
                        @IsDiscountable,@IsDiscontinue,@ReorderLevel,@ModifiedOn,@CreatedOn,@IsDeleted,@Weight,@ItemsType,@PreviousSellingCost,@ExtraCharge,@DiscountLimit)";


            await _destinationConnection.ExecuteAsync(sql, parameter);
           
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
