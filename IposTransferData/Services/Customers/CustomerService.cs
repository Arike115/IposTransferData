using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public CustomerService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM Customer";
            var customers = await _sqlConnection.QueryAsync<Customer>(sql);
            return customers;
        }

        public async Task InsertCustomerAsync(Customer customer)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO Customer (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            LastName, FirstName, Phone1, Phone2, Email, DoB, Address, City, State, Country,
            WalletBalance, Wallet_Id, Business_Id, Store_Id
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @LastName, @FirstName, @Phone1, @Phone2, @Email, @DoB, @Address, @City, @State, @Country,
            @WalletBalance, @Wallet_Id, @Business_Id, @Store_Id
        );";

            await _destinationConnection.ExecuteAsync(sql, customer);
        }
    }
}
