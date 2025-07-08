using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.SaleItems
{
    public interface ISaleItemService
    {
        Task<IEnumerable<SaleItem>> GetSaleItemsAsync();
        Task InsertSaleItemAsync(SaleItem saleItem);
    }
}
