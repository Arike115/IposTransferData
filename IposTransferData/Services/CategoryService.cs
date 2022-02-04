using Dapper;
using IposTransferData.Dto;
using IposTransferData.Model;
using Microsoft.Extensions.Configuration;
using System;   
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IposTransferData.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;
        public CategoryService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategory()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"select * From Category ca
                        WHERE ca.IsDeleted <> 1
                        ORDER BY ca.ModifiedOnUtc DESC";
           
            var category = await _sqlConnection.QueryAsync<CategoryDto>(sql, null);
            return category;

        }

        public async Task<IEnumerable<CategoryItemDto>> GetCategoryItem()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT i.ProductUId, c.CategoryUId From Product i
	                   INNER JOIN Category c ON c.CategoryUId = i.Category_UId
	                    WHERE i.IsDeleted <> 1";

            

            var categoryItem = await _sqlConnection.QueryAsync<CategoryItemDto>(sql, null);
           return categoryItem;

        }

        public async Task InsertCategoryItem(Guid? Item_Id,Guid? Category_Id )
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var parameter = new DynamicParameters();
            parameter.Add("@Item_Id", Item_Id);
            parameter.Add("@Category_Id", Category_Id);
            


            var sql = @"INSERT into CategoryItem(Item_Id, Category_Id) 
                                      Values(@Item_Id, @Category_Id)";

            await _destinationConnection.ExecuteAsync(sql, parameter);


        }


        public async Task InsertCategoryData(Category cat)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var parameter = new DynamicParameters();
            parameter.Add("@Id", cat.Id);
            parameter.Add("@Title", cat.Title);
            parameter.Add("@Description", cat.Description);
            parameter.Add("@ParentCategory_Id", cat.ParentCategory_Id ?? null);
            parameter.Add("@IsDeleted", cat.IsDeleted);
            parameter.Add("@ModifiedOn", cat.ModifiedOn);
            parameter.Add("@CreatedOn", cat.CreatedOn);
            parameter.Add("@LogoFileSize", cat.LogoFileSize ?? 0);

            
            var sql = @"INSERT into Category(Id, Title, Description, ParentCategory_Id,IsDeleted, ModifiedOn, CreatedOn,LogoFileSize) 
                                      Values(@Id, @Title, @Description, @ParentCategory_Id, @IsDeleted, @ModifiedOn, @CreatedOn, @LogoFileSize)";

            await _destinationConnection.ExecuteAsync(sql, parameter);
            
           
        }

      
    }
}
