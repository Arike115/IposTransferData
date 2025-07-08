using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Wallets
{
    public interface IWalletHistoryService
    {
        Task<IEnumerable<WalletHistory>> GetWalletHistoriesAsync();
        Task InsertWalletHistoryAsync(WalletHistory walletHistory);
    }
}
