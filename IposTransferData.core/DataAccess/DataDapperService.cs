using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Core.DataAccess
{
    public class DataDapperService : IDataDapperService
    {
        private readonly IConfiguration _config;

        public DataDapperService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> GetData<T, U>(string sql, U paramaters, string connectionId = "OldConnection")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(sql, paramaters, commandType: CommandType.Text);

        }

        public async Task SaveData<T>(string sql, T paramaters, string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            await connection.ExecuteAsync(sql, paramaters, commandType: CommandType.Text);

        }
    }
}
