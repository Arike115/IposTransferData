using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Settings
{
    public interface IItemTaxService
    {
        Task<IEnumerable<ItemTax>> GetItemTaxesAsync();
        Task InsertItemTaxAsync(ItemTax itemTax);
    }
}
