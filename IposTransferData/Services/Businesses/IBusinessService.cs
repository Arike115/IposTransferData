using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Businesses
{
    public interface IBusinessService
    {
        Task<IEnumerable<Business>> GetBusinessesAsync();
        Task InsertBusinessAsync(Business business);
    }
}
