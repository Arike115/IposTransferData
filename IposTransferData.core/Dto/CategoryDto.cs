using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.core.Dto
{
   public class CategoryDto
    {
        public Guid? CategoryUId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ParentCatId { get; set; }
        public string ParentCatName { get; set; }
    }
}
