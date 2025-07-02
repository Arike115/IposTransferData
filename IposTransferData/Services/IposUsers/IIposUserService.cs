using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.IposUsers
{
    public interface IIposUserService
    {
        Task<IEnumerable<IposUser>> GetUsersAsync();
        Task InsertUserAsync(IposUser user);
    }
}
