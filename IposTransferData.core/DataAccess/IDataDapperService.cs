using System.Collections.Generic;
using System.Threading.Tasks;

namespace IposTransferData.Core.DataAccess
{
    public interface IDataDapperService
    {
        Task<IEnumerable<T>> GetData<T, U>(string sql, U paramaters, string connectionId = "OldConnection");
        Task SaveData<T>(string sql, T paramaters, string connectionId = "Default");
    }
}