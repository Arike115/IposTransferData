using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.DataAccess
{
    public class DataDapperService : IDataDapperService
    {

        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public DataDapperService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<T>> GetData<T>(string sql, object paramaters)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            return await _sqlConnection.QueryAsync<T>(sql, paramaters, commandType: CommandType.Text);

        }

        public async Task SaveData(string sql, object paramaters)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            await _destinationConnection.ExecuteAsync(sql, paramaters, commandType: CommandType.Text);

        }
    }
}
