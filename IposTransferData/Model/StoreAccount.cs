using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class StoreAccount : BaseEntity
    {
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string EmailAddress { get; set; }
        public Guid Login_Id { get; set; }
    }
}
