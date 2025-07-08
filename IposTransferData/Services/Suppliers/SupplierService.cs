using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.Suppliers
{
    public class SupplierService : ISupplierService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public SupplierService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM Supplier ORDER BY Name";
            var suppliers = await _sqlConnection.QueryAsync<Supplier>(sql);
            return suppliers;
        }

        public async Task InsertSupplierAsync(Supplier supplier)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO Supplier (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            Name, Description, RCNumber, VendorNo, Address, ContactName, ContactTel, ContactEmail,
            ContantName2, ContactTel2, ContactEmail2, ContactName3, ContactTel3, ContactEmail3,
            Business_Id, Store_Id
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @Name, @Description, @RCNumber, @VendorNo, @Address, @ContactName, @ContactTel, @ContactEmail,
            @ContantName2, @ContactTel2, @ContactEmail2, @ContactName3, @ContactTel3, @ContactEmail3,
            @Business_Id, @Store_Id
        );";

            await _destinationConnection.ExecuteAsync(sql, supplier);
        }
    }
}
