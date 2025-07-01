using IposTransferData.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class IposUser
    {
        
        public  DateTimeOffset? LockoutEnd { get; set; }
        public  bool TwoFactorEnabled { get; set; }
        public  bool PhoneNumberConfirmed { get; set; }
        public  string? PhoneNumber { get; set; }
        public  string? ConcurrencyStamp { get; set; }
        public  string? SecurityStamp { get; set; }
        public  string? PasswordHash { get; set; }
        public  bool EmailConfirmed { get; set; }
        public string? NormalizedEmail { get; set; }
        public string? Email { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? UserName { get; set; }
        public Guid Id { get; set; }
        public virtual bool LockoutEnabled { get; set; }
        public virtual int AccessFailedCount { get; set; }
        public string RefreshToken { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public long? StaffNo { get; set; }
        public string Department { get; set; }
        public DateTimeOffset CreatedOnUtc { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool Activated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? ModifiedOnUtc { get; set; }
        public bool IsPasswordDefault { get; set; }
        public string Unit { get; set; }
        public Genders Gender { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
        public bool IsBusinessAdmin { get; set; }
        public UserTypes UserType { get; set; }
        public int? RegState { get; set; }
        public string? ReferralCode { get; set; }
        public string? UserPin { get; set; }
        public bool IsPinCreated { get; set; }
        public int SaleCount { get; set; }
        public int InvoiceCount { get; set; }
        public int ProductCount { get; set; }
        public int CustomerCount { get; set; }
        public int PurchaseCount { get; set; }
        public int ReceiptCount { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string TimeZone { get; set; }
        public bool NewStoreAssigned { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
      
    }
}
