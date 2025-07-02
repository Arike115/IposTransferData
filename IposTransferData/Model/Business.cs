using IposTransferData.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Business
    {
        public string UniqueCode { get; set; }
        public string RefCode { get; set; }
        public string Name { get; set; }
        public string NatureOfBusiness { get; set; }
        public string RCNumber { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ContactName { get; set; }
        public string ContactTel { get; set; }
        public string ContactEmail { get; set; }
        public string LogoUrl { get; set; }
        public string LogoName { get; set; }
        public long LogoFileSize { get; set; }
        public string LogoOriginalFileName { get; set; }
        public bool IsActive { get; set; }
        public bool HasBISubscription { get; set; }
        public string SubscriptionId { get; set; }
        public decimal Wallet { get; set; }
        public string CategoryOfBusiness { get; set; }
        public string ReferralCode { get; set; }
        public Guid? BusinessTypeId { get; set; }
        public BusinessType BusinessType { get; set; }
        public Guid? LastActivityUserId { get; set; }
        public DateTimeOffset? LastActivityDate { get; set; }
        public string TimeZone { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
