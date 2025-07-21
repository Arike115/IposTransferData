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

namespace IposTransferData.Services.Categories
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

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"select * from Category where CreatedOn<>'2019-09-16 00:00:00.0000000'";
            var categories = await _sqlConnection.QueryAsync<Category>(sql);
            return categories;
        }

        public async Task InsertCategoryAsync(Category category)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"INSERT INTO Category (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            Title, Description, ParentCategory_Id, LogoFileSize, LogoUrl, LogoName, LogoOriginalFileName, Business_Id, Store_Id
            ) VALUES (
                @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
                @Title, @Description, @ParentCategory_Id, @LogoFileSize, @LogoUrl, @LogoName, @LogoOriginalFileName, @Business_Id, @Store_Id
            );";

            await _destinationConnection.ExecuteAsync(sql, category);
        }
    }
}
