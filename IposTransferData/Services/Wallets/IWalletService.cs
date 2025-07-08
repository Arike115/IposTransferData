using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Wallets
{
    public interface IWalletService
    {
        Task<IEnumerable<Wallet>> GetWalletsAsync();
        Task InsertWalletAsync(Wallet wallet);
    }
}
