using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.PurchaseOrders
{
    public class PurchaseOrderItemService : IPurchaseOrderItemService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public PurchaseOrderItemService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<PurchaseOrderItem>> GetPurchaseOrderItemsAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM PurchaseOrderItem";
            var purchaseOrderItems = await _sqlConnection.QueryAsync<PurchaseOrderItem>(sql);
            return purchaseOrderItems;
        }

        public async Task InsertPurchaseOrderItemAsync(PurchaseOrderItem purchaseOrderItem)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO PurchaseOrderItem (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            PurchaseOrder_Id, Name, Quantity, Price, Discount, Item_Id, CurrentQuantity
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @PurchaseOrder_Id, @Name, @Quantity, @Price, @Discount, @Item_Id, @CurrentQuantity
        );";

            await _destinationConnection.ExecuteAsync(sql, purchaseOrderItem);
        }
    }
}
