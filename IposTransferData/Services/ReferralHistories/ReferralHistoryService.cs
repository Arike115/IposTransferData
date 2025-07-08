using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.ReferralHistories
{
    public class ReferralHistoryService : IReferralHistoryService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public ReferralHistoryService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<ReferralHistory>> GetReferralHistoriesAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM ReferralHistory";
            var histories = await _sqlConnection.QueryAsync<ReferralHistory>(sql);
            return histories;
        }

        public async Task InsertReferralHistoryAsync(ReferralHistory referralHistory)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO ReferralHistory (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            MarketerName, ReferralCode, Email, BusinessId, BusinessName
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @MarketerName, @ReferralCode, @Email, @BusinessId, @BusinessName
        );";

            await _destinationConnection.ExecuteAsync(sql, referralHistory);
        }
    }
}
