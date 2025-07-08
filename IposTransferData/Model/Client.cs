using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public Guid? Store_Id { get; set; }
        public string Email { get; set; }
        public Guid? Business_Id { get; set; }
        public bool IsDisabled { get; set; }
    }
}
