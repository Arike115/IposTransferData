using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Stores
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetStoresAsync();
        Task InsertStoreAsync(Store store);
    }
}
