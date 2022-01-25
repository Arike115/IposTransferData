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
        string connectionString = "Server=DESKTOP-M3U3Q02\\S_SQLEXPRESS;Database=Iposv3;Trusted_Connection=true;MultipleActiveResultSets=false;TrustServerCertificate=True";
        string desString = "Server=DESKTOP-M3U3Q02\\S_SQLEXPRESS;Database=IposDataRetrieved;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True";

        public DataDapperService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _destinationConnection = new SqlConnection(desString); ;
        }
       
        public async Task<IEnumerable<T>> GetData<T>(string sql, object paramaters)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            return  await _sqlConnection.QueryAsync<T>(sql, paramaters, commandType: CommandType.Text);

        }

        public async Task SaveData(string sql, object paramaters)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            await _destinationConnection.ExecuteAsync(sql, paramaters, commandType: CommandType.Text);

        }
    }
}
