using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Store : BaseEntity
    {
        public bool IsActive { get; set; }
        public Guid? Business_Id { get; set; }
        public int NoofTerminal { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ContactName { get; set; }
        public string ContactTel { get; set; }
        public string CurrencyCode { get; set; }
        public string LogoUrl { get; set; }
        public string LogoName { get; set; }
        public long LogoFileSize { get; set; }
        public string LogoOriginalFileName { get; set; }
        public string ContactEmail { get; set; }
        public string Description { get; set; }
        public string TimeZone { get; set; }
        public bool PrimaryLocation { get; set; }
    }
}
