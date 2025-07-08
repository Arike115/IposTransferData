using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.PendingSales
{
    public interface IPendingSaleService
    {
        Task<IEnumerable<PendingSale>> GetPendingSalesAsync();
        Task InsertPendingSaleAsync(PendingSale pendingSale);
    }
}
