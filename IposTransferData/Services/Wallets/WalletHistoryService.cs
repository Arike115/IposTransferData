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
    public class WalletHistoryService : IWalletHistoryService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public WalletHistoryService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<WalletHistory>> GetWalletHistoriesAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM WalletHistory";
            var histories = await _sqlConnection.QueryAsync<WalletHistory>(sql);
            return histories;
        }

        public async Task InsertWalletHistoryAsync(WalletHistory walletHistory)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO WalletHistory (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            Wallet_Id, Type, Amount, TransactionDate, ValueDate, WalletBalance, CurrencyCode,
            LienBalance, WalletHistory_Id
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @Wallet_Id, @Type, @Amount, @TransactionDate, @ValueDate, @WalletBalance, @CurrencyCode,
            @LienBalance, @WalletHistory_Id
        );";

            await _destinationConnection.ExecuteAsync(sql, walletHistory);
        }
    }
}
