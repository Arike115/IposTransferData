using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IposTransferData.Services.PendingSales
{
    public class PendingSaleService : IPendingSaleService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;

        public PendingSaleService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }

        public async Task<IEnumerable<PendingSale>> GetPendingSalesAsync()
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"SELECT * FROM PendingSale";
            var pendingSales = await _sqlConnection.QueryAsync<PendingSale>(sql);
            return pendingSales;
        }

        public async Task InsertPendingSaleAsync(PendingSale pendingSale)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var sql = @"
        INSERT INTO PendingSale (
            Id, CreatedOn, ModifiedOn, DeletedOn, CreatedBy, ModifiedBy, DeletedBy, IsDeleted,
            CustomerDetail, Customer_Id, ParentPendingSale_Id, Sale_Id, NetCost, TotalCost, Discount, NetItemDiscount,
            Title, ApprovalCount, ExtraCharges, Business_Id, Store_Id, StoreDetail, Status, RefNo, CurrencyCode, InvoiceId
        ) VALUES (
            @Id, @CreatedOn, @ModifiedOn, @DeletedOn, @CreatedBy, @ModifiedBy, @DeletedBy, @IsDeleted,
            @CustomerDetail, @Customer_Id, @ParentPendingSale_Id, @Sale_Id, @NetCost, @TotalCost, @Discount, @NetItemDiscount,
            @Title, @ApprovalCount, @ExtraCharges, @Business_Id, @Store_Id, @StoreDetail, @Status, @RefNo, @CurrencyCode, @InvoiceId
        );";

            await _destinationConnection.ExecuteAsync(sql, pendingSale);
        }
    }
}
