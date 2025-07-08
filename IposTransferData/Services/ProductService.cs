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
        public ProductService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection; 
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryId(int category_UId)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"select * From Product po
                        WHERE po.Category_UId = @category_UId";
            var Products =  await _sqlConnection.QueryAsync<ProductDto>(sql, new { Category_UId = category_UId });
            return Products;

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

        
        public async Task InsertProductData(Item Prod)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();
           
            var parameter = new DynamicParameters();
            parameter.Add("@Id", Prod.Id);
            parameter.Add("@Barcode", Prod.Barcode);
            parameter.Add("@Quantity", Prod.Quantity);
            parameter.Add("@Title", Prod.Title);
            parameter.Add("@Description", Prod.Description);
            parameter.Add("@SellingCost", Prod.SellingCost);
            parameter.Add("@ActualCost", Prod.ActualCost);
            parameter.Add("@LogoUrl", Prod.LogoUrl);
            parameter.Add("@LogoOriginalFileName", Prod.LogoOriginalFileName);
            parameter.Add("@LogoFileSize", Prod.LogoFileSize);
            parameter.Add("@IsDiscountable", Prod.IsDiscountable);
            parameter.Add("@IsDiscontinue", Prod.IsDiscontinue);
            parameter.Add("@ReorderLevel", Prod.ReorderLevel);
            parameter.Add("@IsDeleted", Prod.IsDeleted);
            parameter.Add("@ModifiedOn", Prod.ModifiedOn);
            parameter.Add("@CreatedOn", Prod.CreatedOn);
            parameter.Add("@Weight", Prod.Weight);
            parameter.Add("@ItemsType", Prod.ItemsType);
            parameter.Add("@PreviousSellingCost", Prod.PreviousSellingCost);
            parameter.Add("@ExtraCharge", Prod.ExtraCharge);
            parameter.Add("@DiscountLimit", Prod.DiscountLimit);

            var sql = @"INSERT into Item(Id,Barcode,Quantity,Title,Description,SellingCost,ActualCost,LogoUrl,LogoOriginalFileName,
                            LogoFileSize,IsDiscountable,IsDiscontinue,ReorderLevel,ModifiedOn,CreatedOn,IsDeleted,Weight,ItemsType,PreviousSellingCost,ExtraCharge,DiscountLimit) 
                        VALUES (@Id, @Barcode, @Quantity, @title, @Description, @SellingCost, @ActualCost,@LogoUrl,@LogoOriginalFileName,@LogoFileSize,
                        @IsDiscountable,@IsDiscontinue,@ReorderLevel,@ModifiedOn,@CreatedOn,@IsDeleted,@Weight,@ItemsType,@PreviousSellingCost,@ExtraCharge,@DiscountLimit)";

            await _destinationConnection.ExecuteAsync(sql, parameter);
           
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"select * from Item where CreatedBy<>'1989883f-4f99-43bf-a754-239bbbfec00e'";
            var items = await _sqlConnection.QueryAsync<Item>(sql);
            return items;
        }

        public async Task InsertItemAsync(Item item)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO Item (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            Barcode, Quantity, Title, Description, KeyFeatures, Specification, Brand,
            Weight, ItemsType, ReorderLevel, SellingCost, PreviousSellingCost, ActualCost, IsDiscountable,
            DiscountLimit, IsDiscontinue, ItemCode, LogoUrl, LogoName, LogoFileSize, LogoOriginalFileName,
            ExtraCharge, Business_Id, Store_Id, TitleSlug, IsFavourite
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @Barcode, @Quantity, @Title, @Description, @KeyFeatures, @Specification, @Brand,
            @Weight, @ItemsType, @ReorderLevel, @SellingCost, @PreviousSellingCost, @ActualCost, @IsDiscountable,
            @DiscountLimit, @IsDiscontinue, @ItemCode, @LogoUrl, @LogoName, @LogoFileSize, @LogoOriginalFileName,
            @ExtraCharge, @Business_Id, @Store_Id, @TitleSlug, @IsFavourite
        );";

            await _destinationConnection.ExecuteAsync(sql, item);
        }
    }
}
