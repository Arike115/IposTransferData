using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class WalletHistory : BaseEntity
    {
        public Guid Wallet_Id { get; set; }
        [ForeignKey(nameof(Wallet_Id))]
        public Wallet Wallet { get; set; }
        public TransactionTypes Type { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset? TransactionDate { get; set; }
        public DateTimeOffset? ValueDate { get; set; }
        public decimal WalletBalance { get; set; }
        public string CurrencyCode { get; set; }
        public decimal LienBalance { get; set; }
        public Guid? WalletHistory_Id { get; set; }
    }

    public enum TransactionTypes
    {
        DEBIT,
        CREDIT,
        CREDIT_LIEN
    }
}
