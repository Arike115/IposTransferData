using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Supplier
    {
        public long RowNo;

        public string Name { get; set; }
        public string Description { get; set; }
        public string RCNumber { get; set; }
        public string VendorNo { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactTel { get; set; }
        public string ContactEmail { get; set; }
        public string ContantName2 { get; set; }
        public string ContactTel2 { get; set; }
        public string ContactEmail2 { get; set; }
        public string ContactName3 { get; set; }
        public string ContactTel3 { get; set; }
        public string ContactEmail3 { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
