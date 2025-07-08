using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.Wallets
{
    public class WalletService : IWalletService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public WalletService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<Wallet>> GetWalletsAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM Wallet";
            var wallets = await _sqlConnection.QueryAsync<Wallet>(sql);
            return wallets;
        }

        public async Task InsertWalletAsync(Wallet wallet)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO Wallet (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            WalletNo, LienBalance, Balance, IsActive, Business_Id, Store_Id, PhoneNumber, EmailAddress,
            Customer_Id, CurrencyCode, IsPrimary
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @WalletNo, @LienBalance, @Balance, @IsActive, @Business_Id, @Store_Id, @PhoneNumber, @EmailAddress,
            @Customer_Id, @CurrencyCode, @IsPrimary
        );";

            await _destinationConnection.ExecuteAsync(sql, wallet);
        }
    }
}
