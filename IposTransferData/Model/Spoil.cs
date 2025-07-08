using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Spoil : BaseEntity
    {
        public Guid Item_Id { get; set; }
        public string Subject { get; set; }
        public string Remarks { get; set; }
        public double Quantity { get; set; }
        public Guid? ItemBatch_Id { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }
        public DateTimeOffset? ProductionDate { get; set; }
        public bool IsExpired { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
        public string Reason { get; set; }
    }
}
