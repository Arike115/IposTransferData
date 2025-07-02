using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Settings
    {
        public bool IsDefault { get; set; }
        public int? Type { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
        public string Title { get; set; }
        public bool IncludePrice { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
