using IposTransferData.Dto;
using IposTransferData.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Sale : BaseEntity
    {
        public string RefNo { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal NetCost { get; set; }
        public decimal Discount { get; set; }
        public decimal NetItemDiscount { get; set; }
        public decimal Tax { get; set; }
        public decimal SumQuantity { get; set; }
        public decimal ExtraCharges { get; set; }
        public int Status { get; set; }
        public string CustomerDetail { get; set; }
        public Guid? Customer_Id { get; set; }
        public PaymentCategory PaymentCategory { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
        public string StoreDetail { get; set; }
        public string Remarks { get; set; }
        public DateTimeOffset? TransactionDate { get; set; }
        public DateTimeOffset? ValueDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public decimal AmountTender { get; set; }
        public decimal OtherPaymentAmount { get; set; }
        public decimal WalletAmount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
