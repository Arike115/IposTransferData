using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.IposRoles
{
    public interface IIposUserRoleService
    {
        Task<IEnumerable<IposUserRole>> GetUserRolesAsync();
        Task InsertUserRoleAsync(IposUserRole userRole);
    }
}
