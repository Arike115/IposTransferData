using IposTransferData.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Payment
    {
        public Guid? Id { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public PaymentCategory PaymentCategory { get; set; }
        public double PaymentAmount { get; set; }
        public Guid? Sale_Id { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
