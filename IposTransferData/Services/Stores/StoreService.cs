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
    public class StoreService : IStoreService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public StoreService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<Store>> GetStoresAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM Store WHERE Name <> 'Ogba Branch' AND Name <> 'Agege Branch' order by Name";
            var stores = await _sqlConnection.QueryAsync<Store>(sql);
            return stores;
        }

        public async Task<Store> GetStoreByBusiness(Guid businessId)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT TOP 1 * FROM Store WHERE Business_Id = @BusinessId";
            var stores = await _sqlConnection.QueryAsync<Store>(sql, new
            {
                BusinessId = businessId
            });
            return stores.FirstOrDefault();
        }

        public async Task InsertStoreAsync(Store store)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO Store (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            IsActive, Business_Id, NoofTerminal, Name, Address, City, Country, State,
            ContactName, ContactTel, CurrencyCode, LogoUrl, LogoName, LogoFileSize, LogoOriginalFileName,
            ContactEmail, Description, TimeZone, PrimaryLocation
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @IsActive, @Business_Id, @NoofTerminal, @Name, @Address, @City, @Country, @State,
            @ContactName, @ContactTel, @CurrencyCode, @LogoUrl, @LogoName, @LogoFileSize, @LogoOriginalFileName,
            @ContactEmail, @Description, @TimeZone, @PrimaryLocation
        );";

            await _destinationConnection.ExecuteAsync(sql, store);
        }
    }

}
