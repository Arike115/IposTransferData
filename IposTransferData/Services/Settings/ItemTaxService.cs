using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.Settings
{
    public class ItemTaxService : IItemTaxService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public ItemTaxService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<ItemTax>> GetItemTaxesAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM ItemTax";
            var itemTaxes = await _sqlConnection.QueryAsync<ItemTax>(sql);
            return itemTaxes;
        }

        public async Task InsertItemTaxAsync(ItemTax itemTax)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO ItemTax (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            Item_Id, Settings_Id, ItemBatch_Id, Business_Id, Store_Id
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @Item_Id, @Settings_Id, @ItemBatch_Id, @Business_Id, @Store_Id
        );";

            await _destinationConnection.ExecuteAsync(sql, itemTax);
        }
    }
}
