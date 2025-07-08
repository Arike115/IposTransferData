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
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public PurchaseOrderService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM PurchaseOrder";
            var purchaseOrders = await _sqlConnection.QueryAsync<PurchaseOrder>(sql);
            return purchaseOrders;
        }

        public async Task InsertPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO PurchaseOrder (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            Supplier_Id, TotalAmount, DueDate, DeliveredDate, Status, RefNo, DeliveryAddressLine1,
            DeliveryAddressLine2, City, State, Country, Business_Id, Store_Id
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @Supplier_Id, @TotalAmount, @DueDate, @DeliveredDate, @Status, @RefNo, @DeliveryAddressLine1,
            @DeliveryAddressLine2, @City, @State, @Country, @Business_Id, @Store_Id
        );";

            await _destinationConnection.ExecuteAsync(sql, purchaseOrder);
        }
    }
}
