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
    public class StoreAccountService : IStoreAccountService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public StoreAccountService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<StoreAccount>> GetStoreAccountsAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM StoreAccount";
            var storeAccounts = await _sqlConnection.QueryAsync<StoreAccount>(sql);
            return storeAccounts;
        }

        public async Task InsertStoreAccountAsync(StoreAccount storeAccount)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO StoreAccount (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            Title, FullName, Department, EmailAddress, Login_Id
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @Title, @FullName, @Department, @EmailAddress, @Login_Id
        );";

            await _destinationConnection.ExecuteAsync(sql, storeAccount);
        }
    }
}
