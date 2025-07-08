using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.Businesses
{
    public class BusinessService : IBusinessService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public BusinessService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<Business>> GetBusinessesAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM Business WHERE Name <> 'Grace Mart' AND Name <> 'Beclean Laundry' order by Name";
            var businesses = await _sqlConnection.QueryAsync<Business>(sql);
            return businesses;
        }

        public async Task InsertBusinessAsync(Business business)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO Business (Id, UniqueCode, RefCode, Name, NatureOfBusiness, RCNumber, Address,
            State, Country, ContactName, ContactTel, ContactEmail, LogoUrl,
            LogoName, LogoFileSize, LogoOriginalFileName, IsActive,
            HasBISubscription, SubscriptionId, Wallet, CategoryOfBusiness,
            ReferralCode, BusinessTypeId, LastActivityUserId, LastActivityDate,
            TimeZone, CreatedOn, ModifiedOn, CreatedBy, ModifiedBy, IsDeleted, DeletedBy, DeletedOn
        ) VALUES (@Id, @UniqueCode, @RefCode, @Name, @NatureOfBusiness, @RCNumber, @Address,
            @State, @Country, @ContactName, @ContactTel, @ContactEmail, @LogoUrl,
            @LogoName, @LogoFileSize, @LogoOriginalFileName, @IsActive,
            @HasBISubscription, @SubscriptionId, @Wallet, @CategoryOfBusiness,
            @ReferralCode, @BusinessTypeId, @LastActivityUserId, @LastActivityDate,
            @TimeZone, @CreatedOn, @ModifiedOn, @CreatedBy, @ModifiedBy, @IsDeleted, @DeletedBy, @DeletedOn
        );";

            await _destinationConnection.ExecuteAsync(sql, business);
        }
    }

}
