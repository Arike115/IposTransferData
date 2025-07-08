using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Audits
{
    public interface IAuditService
    {
        Task<IEnumerable<Audit>> GetAuditsAsync();
        Task InsertAuditAsync(Audit audit);
    }
}
