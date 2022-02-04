using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Dto
{
   public class CategoryDto
    {
        public int CategoryUId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ParentCatId { get; set; }
        public string ParentCatName { get; set; }
        public DateTime? CreatedOn { get; set; }

    }


    public class CategoryItemDto 
    {
        public int CategoryUId { get; set; }
        public Guid ProductUId { get; set; }
    }

}
