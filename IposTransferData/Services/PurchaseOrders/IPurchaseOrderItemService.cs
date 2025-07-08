using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.PurchaseOrders
{
    public interface IPurchaseOrderItemService
    {
        Task<IEnumerable<PurchaseOrderItem>> GetPurchaseOrderItemsAsync();
        Task InsertPurchaseOrderItemAsync(PurchaseOrderItem purchaseOrderItem);
    }
}
