using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.ReferralHistories
{
    public interface IReferralHistoryService
    {
        Task<IEnumerable<ReferralHistory>> GetReferralHistoriesAsync();
        Task InsertReferralHistoryAsync(ReferralHistory referralHistory);
    }
}
