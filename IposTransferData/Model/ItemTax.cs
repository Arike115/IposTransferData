using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class ItemTax : BaseEntity
    {
        public Guid? Item_Id { get; set; }
        public Guid? Settings_Id { get; set; }
        public Guid? ItemBatch_Id { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
    }
}
