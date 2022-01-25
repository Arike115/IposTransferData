using System.Collections.Generic;
using System.Threading.Tasks;

namespace IposTransferData.DataAccess
{
    public interface IDataDapperService
    {
        Task<IEnumerable<T>> GetData<T>(string sql, object paramaters);
        Task SaveData(string sql, object paramaters);
    }
}