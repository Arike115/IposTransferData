using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.ItemBatches
{
    public class ItemBatchService : IItemBatchService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public ItemBatchService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<ItemBatch>> GetItemBatchesAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM ItemBatch";
            var itemBatches = await _sqlConnection.QueryAsync<ItemBatch>(sql);
            return itemBatches;
        }

        public async Task<ItemBatch> GetItemBatchByItemAsync(Guid ItemId)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"select TOP 1 * from ItemBatch WHERE Item_Id=@ItemId AND IsDeleted <> 1 Order By ModifiedOn DESC";
            var itemBatches = await _sqlConnection.QueryAsync<ItemBatch>(sql, new
            {
                ItemId = ItemId,
            });
            return itemBatches.FirstOrDefault();
        }

        public async Task<ItemBatch> GetActiveItemBatchByItemAsync(Guid ItemId)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"select TOP 1 * from ItemBatch WHERE Item_Id=@ItemId AND  BatchStatus=1 AND IsDeleted <> 1";
            var itemBatches = await _destinationConnection.QueryAsync<ItemBatch>(sql, new
            {
                ItemId = ItemId,
            });
            return itemBatches.FirstOrDefault();
        }

        public async Task ActivateItemBatchAsync(ItemBatchStatuses BatchStatus, Guid BatchId)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"Update ItemBatch SET BatchStatus = @BatchStatus WHERE Id = @Id";
            await _destinationConnection.ExecuteAsync(sql, new
            {
                BatchStatus = BatchStatus,
                Id = BatchId
            });
        }

        public async Task InsertItemBatchAsync(ItemBatch itemBatch)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO ItemBatch (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            IsActive, Item_Id, Price, BestBefore, MadeOn, Quantity, StockupSource, PreviousQuantity,
            PreviouStockDate, PreviousItemBatch_Id, PurchaseOrder_Id, Business_Id, Store_Id,
            ReorderLevel, CostPrice, OriginalAmount, Discount, BatchStatus
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @IsActive, @Item_Id, @Price, @BestBefore, @MadeOn, @Quantity, @StockupSource, @PreviousQuantity,
            @PreviouStockDate, @PreviousItemBatch_Id, @PurchaseOrder_Id, @Business_Id, @Store_Id,
            @ReorderLevel, @CostPrice, @OriginalAmount, @Discount, @BatchStatus
        );";

            await _destinationConnection.ExecuteAsync(sql, itemBatch);
        }
    }
}
