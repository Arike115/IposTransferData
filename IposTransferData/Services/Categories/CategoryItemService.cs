using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.Categories
{
    public class CategoryItemService : ICategoryItemService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public CategoryItemService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<CategoryItem>> GetCategoryItemsAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"select * from CategoryItem where 
                        NOT (
                        (Category_Id ='2531B9C3-4529-4CAD-B4B2-B09963A4A2D6' AND Item_Id='5B4CF0E5-6808-4775-B066-28E08EC26117') OR
                        (Category_Id ='EA161438-A438-403B-B10A-E960FCB6AA92' AND Item_Id='2E544DA3-4B76-4C9E-9E4D-2948E61E52E9') OR
                        (Category_Id ='A8F18E8E-4EB1-4486-8A69-9743F952AF7A' AND Item_Id='66346ADB-20E5-425D-A330-47CF649CF44D') OR
                        (Category_Id ='A8F18E8E-4EB1-4486-8A69-9743F952AF7A' AND Item_Id='A6F1027F-83D0-483C-81FA-8F42152DA940') OR
                        (Category_Id ='A8F18E8E-4EB1-4486-8A69-9743F952AF7A' AND Item_Id='B4B1F584-EB69-4DD4-9D15-9B0ADB17372A') 
                        )";
            var categoryItems = await _sqlConnection.QueryAsync<CategoryItem>(sql);
            return categoryItems;
        }

        public async Task InsertCategoryItemAsync(CategoryItem categoryItem)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO CategoryItem (
            Item_Id, Category_Id
        ) VALUES (
            @Item_Id, @Category_Id
        );";

            await _destinationConnection.ExecuteAsync(sql, categoryItem);
        }
    }
}
