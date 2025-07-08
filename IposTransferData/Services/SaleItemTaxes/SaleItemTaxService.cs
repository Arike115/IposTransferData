using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.SaleItemTaxes
{
    public class SaleItemTaxService : ISaleItemTaxService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public SaleItemTaxService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<SaleItemTax>> GetSaleItemTaxesAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM SaleItemTax";
            var saleItemTaxes = await _sqlConnection.QueryAsync<SaleItemTax>(sql);
            return saleItemTaxes;
        }

        public async Task InsertSaleItemTaxAsync(SaleItemTax saleItemTax)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO SaleItemTax (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            SaleItem_Id, Settings_Id, TaxName, TaxRate, TaxAmount, SalesPrice, TaxType,
            Business_Id, Store_Id, CurrencyCode
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @SaleItem_Id, @Settings_Id, @TaxName, @TaxRate, @TaxAmount, @SalesPrice, @TaxType,
            @Business_Id, @Store_Id, @CurrencyCode
        );";

            await _destinationConnection.ExecuteAsync(sql, saleItemTax);
        }
    }
}
