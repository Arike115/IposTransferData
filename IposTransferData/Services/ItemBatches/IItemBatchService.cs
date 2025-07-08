using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.ItemBatches
{
    public interface IItemBatchService
    {
        Task<IEnumerable<ItemBatch>> GetItemBatchesAsync();
        Task InsertItemBatchAsync(ItemBatch itemBatch);
        Task ActivateItemBatchAsync(ItemBatchStatuses BatchStatus, Guid BatchId);
        Task<ItemBatch> GetItemBatchByItemAsync(Guid ItemId);
        Task<ItemBatch> GetActiveItemBatchByItemAsync(Guid ItemId);
    }
}
