using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Stores
{
    public interface IStoreNAccountService
    {
        Task<IEnumerable<StoreNAccount>> GetStoreNAccountsAsync();
        Task InsertStoreNAccountAsync(StoreNAccount storeNAccount);
    }
}
