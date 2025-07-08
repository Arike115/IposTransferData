using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class ReferralHistory : BaseEntity
    {
        public string MarketerName { get; set; }
        public string ReferralCode { get; set; }
        public string Email { get; set; }
        public Guid? BusinessId { get; set; }
        public string BusinessName { get; set; }
    }
}
