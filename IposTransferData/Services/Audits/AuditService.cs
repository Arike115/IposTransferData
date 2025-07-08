using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.Audits
{
    public class AuditService : IAuditService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public AuditService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<Audit>> GetAuditsAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM Audit";
            var audits = await _sqlConnection.QueryAsync<Audit>(sql);
            return audits;
        }

        public async Task InsertAuditAsync(Audit audit)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO Audit (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            PrimaryKey, UserId, ActionType, Description, HttpMethod, OldValues, NewValues, TableName,
            IPAddress, AreaAccessed, TraceId, BrowserInfo, UserName, BusinessId, StoreId
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @PrimaryKey, @UserId, @ActionType, @Description, @HttpMethod, @OldValues, @NewValues, @TableName,
            @IPAddress, @AreaAccessed, @TraceId, @BrowserInfo, @UserName, @BusinessId, @StoreId
        );";

            await _destinationConnection.ExecuteAsync(sql, audit);
        }
    }
}
