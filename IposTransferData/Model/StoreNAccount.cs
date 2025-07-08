using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class StoreNAccount : BaseEntity
    {
        public Guid Store_Id { get; set; }
        public Guid StoreAccount_Id { get; set; }
    }
}
