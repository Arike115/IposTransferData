using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.SaleItems
{
    public class SaleItemService : ISaleItemService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public SaleItemService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<SaleItem>> GetSaleItemsAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM SaleItem";
            var saleItems = await _sqlConnection.QueryAsync<SaleItem>(sql);
            return saleItems;
        }

        public async Task InsertSaleItemAsync(SaleItem saleItem)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO SaleItem (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            PendingSale_Id, Sale_Id, Item_Id, InvoiceSale_Id, Name, Description, Code, Quantity, RecallQuantity,
            TotalTransactionQuantity, CostPrice, Price, PreviousQuantity, ActiveItemBatch_Id, WasDiscounted,
            Discount, ExtraCharge, Remarks, CurrencyCode, SalesStatus
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @PendingSale_Id, @Sale_Id, @Item_Id, @InvoiceSale_Id, @Name, @Description, @Code, @Quantity, @RecallQuantity,
            @TotalTransactionQuantity, @CostPrice, @Price, @PreviousQuantity, @ActiveItemBatch_Id, @WasDiscounted,
            @Discount, @ExtraCharge, @Remarks, @CurrencyCode, @SalesStatus
        );";

            await _destinationConnection.ExecuteAsync(sql, saleItem);
        }
    }
}
