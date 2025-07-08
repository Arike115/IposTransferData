using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class SaleItemTax : BaseEntity
    {
        public Guid SaleItem_Id { get; set; }
        public Guid Settings_Id { get; set; }
        public string TaxName { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal SalesPrice { get; set; }
        public int TaxType { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
        public string CurrencyCode { get; set; }

    }
}
