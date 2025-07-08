using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IposTransferData.Model;
using Dapper;

namespace IposTransferData.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public SettingsService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<Setting>> GetSettingsAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM Setting WHERE NOT
                                (
                                (Description = 'VAT TAX' AND CreatedBy='systemuser@ipos.com') OR
                                (Description = 'Extra Charges' AND CreatedBy='systemuser@ipos.com') OR
                                (Description = 'Currency Symbol' AND CreatedBy='systemuser@ipos.com') OR
                                (Description = 'Currency Code' AND CreatedBy='systemuser@ipos.com')
                                )";
            var settingsList = await _sqlConnection.QueryAsync<Setting>(sql);
            return settingsList;
        }

        public async Task InsertSettingsAsync(Setting settings)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"INSERT INTO Setting (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            IsDefault, Type, Description, Value, Business_Id, Store_Id, Title, IncludePrice
            ) VALUES (
                @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
                @IsDefault, @Type, @Description, @Value, @Business_Id, @Store_Id, @Title, @IncludePrice
            );";

            await _destinationConnection.ExecuteAsync(sql, settings);
        }
    }
}
