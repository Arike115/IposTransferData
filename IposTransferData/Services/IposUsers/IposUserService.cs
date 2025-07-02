using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IposTransferData.Model;

namespace IposTransferData.Services.IposUsers
{
    public class IposUserService : IIposUserService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public IposUserService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<IposUser>> GetUsersAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM IposUser";

            var users = await _sqlConnection.QueryAsync<IposUser>(sql, null);
            return users;
        }

        public async Task InsertUserAsync(IposUser user)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
            INSERT INTO IposUser (
                Id, RefreshToken, LastName, FirstName, MiddleName, StaffNo, Department,
                CreatedOnUtc, LastLoginDate, CreatedBy, ModifiedBy, Activated, IsDeleted,
                ModifiedOnUtc, IsPasswordDefault, Unit, Gender, Business_Id, Store_Id,
                IsBusinessAdmin, UserType, RegState, ReferralCode, UserPin, IsPinCreated,
                SaleCount, InvoiceCount, ProductCount, CustomerCount, PurchaseCount,
                ReceiptCount, State, Country, Address, TimeZone, NewStoreAssigned,
                UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed,
                PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber,
                PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled,
                AccessFailedCount
            )
            VALUES (
                @Id, @RefreshToken, @LastName, @FirstName, @MiddleName, @StaffNo, @Department,
                @CreatedOnUtc, @LastLoginDate, @CreatedBy, @ModifiedBy, @Activated, @IsDeleted,
                @ModifiedOnUtc, @IsPasswordDefault, @Unit, @Gender, @Business_Id, @Store_Id,
                @IsBusinessAdmin, @UserType, @RegState, @ReferralCode, @UserPin, @IsPinCreated,
                @SaleCount, @InvoiceCount, @ProductCount, @CustomerCount, @PurchaseCount,
                @ReceiptCount, @State, @Country, @Address, @TimeZone, @NewStoreAssigned,
                @UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed,
                @PasswordHash, @SecurityStamp, @ConcurrencyStamp, @PhoneNumber,
                @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEnd, @LockoutEnabled,
                @AccessFailedCount
            );";

            await _destinationConnection.ExecuteAsync(sql, user);
        }
    }
}
