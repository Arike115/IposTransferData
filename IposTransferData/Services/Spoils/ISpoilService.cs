using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Spoils
{
    public interface ISpoilService
    {
        Task<IEnumerable<Spoil>> GetSpoilsAsync();
        Task InsertSpoilAsync(Spoil spoil);
    }
}
