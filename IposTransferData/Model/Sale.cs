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
    public class Sale
    {
        public Guid? Id { get; set; }
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
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }


        public static explicit operator Sale(SaleDto source)
        {
            var destination = new Sale();
            destination.Id = source.Id;
            destination.RefNo = source.RefNo;
            destination.NetPrice = source.NetPrice;
            destination.NetCost = source.NetCost;
            destination.Cost = source.Cost;
            destination.Discount = source.Discount;
            destination.NetItemDiscount = source.NetItemDiscount;
            destination.SumQuantity = source.SumQuantity;
            destination.ExtraCharges = source.ExtraCharges;
            destination.Customer_Id = source.Customer_Id;
            destination.CustomerDetail = source.CustomerDetail;
            destination.PaymentType = source.PaymentType;
            destination.Business_Id = source.Business_Id;
            destination.Status = source.Status;
            destination.Store_Id = source.Store_Id;
            destination.StoreDetail = source.StoreDetail;
            destination.Remarks = source.Remarks;
            destination.TransactionDate = source.TransactionDate;
            destination.ValueDate = source.ValueDate;
            destination.DueDate = source.DueDate;
            destination.AmountTender = source.AmountTender;
            destination.CurrencyCode = source.CurrencyCode;
            destination.CreatedBy = source.CreatedBy;
            destination.ModifiedBy = source.ModifiedBy;
            destination.CreatedOn = source.CreatedOn;
            destination.ModifiedOn = source.ModifiedOn;
            destination.IsDeleted = source.IsDeleted;
            return destination;
        }
    }
}
