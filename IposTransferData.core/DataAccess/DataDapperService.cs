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
        private readonly SqlConnection _connection;

        public DataDapperService(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> GetData<T>(string sql, object paramaters)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            return await _connection.QueryAsync<T>(sql, paramaters, commandType: CommandType.Text);

        }

        public async Task SaveData(string sql, object paramaters)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            await _connection.ExecuteAsync(sql, paramaters, commandType: CommandType.Text);

        }
    }
}
