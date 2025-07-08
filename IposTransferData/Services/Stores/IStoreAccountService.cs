using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Stores
{
    public interface IStoreAccountService
    {
        Task<IEnumerable<StoreAccount>> GetStoreAccountsAsync();
        Task InsertStoreAccountAsync(StoreAccount storeAccount);
    }
}
