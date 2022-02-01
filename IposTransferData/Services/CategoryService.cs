using Dapper;
using IposTransferData.Dto;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IposTransferData.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;
        string connectionString = "Server=DESKTOP-M3U3Q02\\S_SQLEXPRESS;Database=Iposv3;Trusted_Connection=true;MultipleActiveResultSets=false;TrustServerCertificate=True";
        string desString = "Server=DESKTOP-M3U3Q02\\S_SQLEXPRESS;Database=Ipos_Transfer;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True";
        public CategoryService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _destinationConnection = new SqlConnection(desString);
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

        public async Task<IEnumerable<CategoryItem>> GetCategoryItem()
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"SELECT i.Id Item_Id, c.Id Category_Id From Item i
	                    JOIN Category c ON c.Id = c.Id
	                    WHERE i.IsDeleted <> 1";

            var categoryItem = await _destinationConnection.QueryAsync<CategoryItem>(sql, null);
            return categoryItem;

        }

        public async Task InsertCategoryItem(Guid Item_Id,Guid Category_Id )
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


        public async Task InsertCategoryData(Guid Id,string Title, string Description,  Guid? ParentCategory_Id, bool IsDeleted, DateTime? ModifiedOn, DateTime? CreatedOn, long? LogoFileSize)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var parameter = new DynamicParameters();
            parameter.Add("@Id", Id);
            parameter.Add("@Title", Title);
            parameter.Add("@Description", Description);
            parameter.Add("@ParentCategory_Id", ParentCategory_Id ?? null);
            parameter.Add("@IsDeleted", IsDeleted);
            parameter.Add("@ModifiedOn", ModifiedOn);
            parameter.Add("@CreatedOn", CreatedOn);
            parameter.Add("@LogoFileSize", LogoFileSize ?? 0);

            
            var sql = @"INSERT into Category(Id, Title, Description, ParentCategory_Id,IsDeleted, ModifiedOn, CreatedOn,LogoFileSize) 
                                      Values(@Id, @Title, @Description, @ParentCategory_Id, @IsDeleted, @ModifiedOn, @CreatedOn, @LogoFileSize)";

            await _destinationConnection.ExecuteAsync(sql, parameter);
            
           
        }

      
    }
}
