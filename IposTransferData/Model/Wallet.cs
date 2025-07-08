using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Wallet : BaseEntity
    {
        public string WalletNo { get; set; }
        public double LienBalance { get; set; }
        public double Balance { get; set; }
        public bool IsActive { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public Guid Customer_Id { get; set; }
        public string CurrencyCode { get; set; }
        public bool IsPrimary { get; set; }
    }
}
