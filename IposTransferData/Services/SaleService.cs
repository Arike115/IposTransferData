using Dapper;
using IposTransferData.Dto;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IposTransferData.Enum;

namespace IposTransferData.Services
{
    public class SaleService : IsaleService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlConnection _destinationConnection;
        public SaleService(SqlConnection sqlConnection, SqlConnection destinationConnection)
        {
            _sqlConnection = sqlConnection;
            _destinationConnection = destinationConnection;
        }


        public async Task InsertProductData(Item Prod)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var parameter = new DynamicParameters();
            parameter.Add("@Id", Prod.Id);
            parameter.Add("@Barcode", Prod.Barcode);
            parameter.Add("@Quantity", Prod.Quantity);
            parameter.Add("@Title", Prod.Title);
            parameter.Add("@Description", Prod.Description);
            parameter.Add("@SellingCost", Prod.SellingCost);
            parameter.Add("@ActualCost", Prod.ActualCost);
            parameter.Add("@LogoUrl", Prod.LogoUrl);
            parameter.Add("@LogoOriginalFileName", Prod.LogoOriginalFileName);
            parameter.Add("@LogoFileSize", Prod.LogoFileSize);
            parameter.Add("@IsDiscountable", Prod.IsDiscountable);
            parameter.Add("@IsDiscontinue", Prod.IsDiscontinue);
            parameter.Add("@ReorderLevel", Prod.ReorderLevel);
            parameter.Add("@IsDeleted", Prod.IsDeleted);
            parameter.Add("@ModifiedOn", Prod.ModifiedOn);
            parameter.Add("@CreatedOn", Prod.CreatedOn);
            parameter.Add("@Weight", Prod.Weight);
            parameter.Add("@ItemsType", Prod.ItemsType);
            parameter.Add("@PreviousSellingCost", Prod.PreviousSellingCost);
            parameter.Add("@ExtraCharge", Prod.ExtraCharge);
            parameter.Add("@DiscountLimit", Prod.DiscountLimit);

            var sql = @"INSERT into Item(Id,Barcode,Quantity,Title,Description,SellingCost,ActualCost,LogoUrl,LogoOriginalFileName,
                            LogoFileSize,IsDiscountable,IsDiscontinue,ReorderLevel,ModifiedOn,CreatedOn,IsDeleted,Weight,ItemsType,PreviousSellingCost,ExtraCharge,DiscountLimit) 
                        VALUES (@Id, @Barcode, @Quantity, @title, @Description, @SellingCost, @ActualCost,@LogoUrl,@LogoOriginalFileName,@LogoFileSize,
                        @IsDiscountable,@IsDiscontinue,@ReorderLevel,@ModifiedOn,@CreatedOn,@IsDeleted,@Weight,@ItemsType,@PreviousSellingCost,@ExtraCharge,@DiscountLimit)";

            await _destinationConnection.ExecuteAsync(sql, parameter);

        }

        public async Task<IEnumerable<SaleDto>> GetSale()
        {

            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();
            var sql = @"select * From sale p
                        WHERE p.IsDeleted <> 1
                        ORDER BY p.ModifiedOn DESC";

            var sale = await _sqlConnection.QueryAsync<SaleDto>(sql, null);
            return sale;
        }

        public async Task<IEnumerable<SaleDto>> GetSaleById(string saleid)
        {
            if (_sqlConnection.State != ConnectionState.Open)
                _sqlConnection.Open();

            var sql = @"select * From sale po
                        WHERE po.Id = @id";
            var sale = await _sqlConnection.QueryAsync<SaleDto>(sql, new { id = saleid });
            return sale;

        }

        public async Task InsertSaleData(Sale Prod)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var parameter = new DynamicParameters();
            parameter.Add("@Id", Prod.Id);
            parameter.Add("@RefNo", Prod.RefNo);
            parameter.Add("@NetPrice", Prod.NetPrice);
            parameter.Add("@Cost", Prod.Cost);
            parameter.Add("@NetCost", Prod.NetCost);
            parameter.Add("@NetItemDiscount", Prod.NetItemDiscount);
            parameter.Add("@Tax", Prod.Tax);
            parameter.Add("@SumQuantity", Prod.SumQuantity);
            parameter.Add("@ExtraCharges", Prod.ExtraCharges);
            parameter.Add("@Discount", Prod.Discount);
            parameter.Add("@Status", Prod.Status);
            parameter.Add("@CustomerDetail", Prod.CustomerDetail);
            parameter.Add("@Customer_Id", Prod.Customer_Id);
            parameter.Add("@PaymentCategory", (int)PaymentCategory.SINGLEPAYEMENT);
            parameter.Add("@IsDeleted", Prod.IsDeleted);
            parameter.Add("@ModifiedOn", Prod.ModifiedOn);
            parameter.Add("@CreatedOn", Prod.CreatedOn);
            parameter.Add("@Business_Id", Prod.Business_Id);
            parameter.Add("@Store_Id", Prod.Store_Id);
            parameter.Add("@StoreDetail", Prod.StoreDetail);
            parameter.Add("@Remarks", Prod.Remarks);
            parameter.Add("@TransactionDate", Prod.TransactionDate);
            parameter.Add("@ValueDate", Prod.ValueDate);
            parameter.Add("@DueDate", Prod.DueDate);
            parameter.Add("@AmountTender", Prod.AmountTender);
            parameter.Add("@OtherPaymentAmount", 0);
            parameter.Add("@WalletAmount", 0);
            parameter.Add("@CurrencyCode", Prod.CurrencyCode);
            parameter.Add("@CreatedBy", Prod.CreatedBy);
            parameter.Add("@ModifiedBy", Prod.ModifiedBy);


                            var sqls = @"INSERT INTO Sale (
                    Id, RefNo, NetPrice, Cost, NetCost, NetItemDiscount, Tax, SumQuantity,
                    ExtraCharges, Discount, Status, CustomerDetail, Customer_Id, PaymentCategory,
                    IsDeleted, ModifiedOn, CreatedOn, Business_Id, Store_Id, StoreDetail, Remarks,
                    TransactionDate, ValueDate, DueDate, AmountTender, OtherPaymentAmount, WalletAmount,
                    CurrencyCode, CreatedBy, ModifiedBy)
                VALUES (
                    @Id, @RefNo, @NetPrice, @Cost, @NetCost, @NetItemDiscount, @Tax, @SumQuantity,
                    @ExtraCharges, @Discount, @Status, @CustomerDetail, @Customer_Id, @PaymentCategory,
                    @IsDeleted, @ModifiedOn, @CreatedOn, @Business_Id, @Store_Id, @StoreDetail, @Remarks,
                    @TransactionDate, @ValueDate, @DueDate, @AmountTender, @OtherPaymentAmount, @WalletAmount,
                    @CurrencyCode, @CreatedBy, @ModifiedBy)";


            await _destinationConnection.ExecuteAsync(sqls, parameter);
        }

        public async Task InsertPaymentDate(Payment pay)
        {
            if (_destinationConnection.State != ConnectionState.Open)
                _destinationConnection.Open();

            var parameter = new DynamicParameters();
            parameter.Add("@Id", pay.Id);
            parameter.Add("@PaymentAmount", pay.PaymentAmount);
            parameter.Add("@Sale_Id", pay.Sale_Id);
            parameter.Add("@PaymentType", pay.PaymentType);
            parameter.Add("@PaymentCategory", (int)PaymentCategory.SINGLEPAYEMENT);
            parameter.Add("@IsDeleted", pay.IsDeleted);
            parameter.Add("@ModifiedOn", pay.ModifiedOn);
            parameter.Add("@CreatedOn", pay.CreatedOn);
            parameter.Add("@CreatedBy", pay.CreatedBy);
            parameter.Add("@ModifiedBy", pay.ModifiedBy);


            var sql = @"INSERT into Payment(Id,PaymentAmount,Sale_Id,PaymentType,PaymentCategory,IsDeleted,ModifiedOn,CreatedOn,CreatedBy,ModifiedBy) 
                        VALUES (@Id,@PaymentAmount,@Sale_Id,@PaymentType,@PaymentCategory,@IsDeleted,@ModifiedOn,@CreatedOn,@CreatedBy,@ModifiedBy)";

            await _destinationConnection.ExecuteAsync(sql, parameter);
        }
    }
}
