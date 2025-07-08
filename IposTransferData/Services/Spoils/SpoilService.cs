using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.Spoils
{
    public class SpoilService : ISpoilService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public SpoilService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<Spoil>> GetSpoilsAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM Spoil";
            var spoils = await _sqlConnection.QueryAsync<Spoil>(sql);
            return spoils;
        }

        public async Task InsertSpoilAsync(Spoil spoil)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO Spoil (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            Item_Id, Subject, Remarks, Quantity, ItemBatch_Id, ExpiryDate, ProductionDate, IsExpired,
            Business_Id, Store_Id, Reason
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @Item_Id, @Subject, @Remarks, @Quantity, @ItemBatch_Id, @ExpiryDate, @ProductionDate, @IsExpired,
            @Business_Id, @Store_Id, @Reason
        );";

            await _destinationConnection.ExecuteAsync(sql, spoil);
        }
    }
}
