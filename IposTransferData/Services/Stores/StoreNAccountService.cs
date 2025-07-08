using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.Stores
{
    public class StoreNAccountService : IStoreNAccountService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public StoreNAccountService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<StoreNAccount>> GetStoreNAccountsAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM StoreNAccount";
            var storeNAccounts = await _sqlConnection.QueryAsync<StoreNAccount>(sql);
            return storeNAccounts;
        }

        public async Task InsertStoreNAccountAsync(StoreNAccount storeNAccount)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO StoreNAccount (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            Store_Id, StoreAccount_Id
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @Store_Id, @StoreAccount_Id
        );";

            await _destinationConnection.ExecuteAsync(sql, storeNAccount);
        }
    }
}
