using IposTransferData.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentCategory_Id { get; set; }
        public long? LogoFileSize { get; set; }
        public Category ParentCategory { get; set; }
        public string LogoUrl { get; set; }
        public string LogoName { get; set; }
        public string LogoOriginalFileName { get; set; }
        public Guid? Business_Id { get; set; }
        public Guid? Store_Id { get; set; }
    }

    public class CategoryItem
    {
        public Guid? Item_Id { get; set; }
        public Guid? Category_Id { get; set; }
    }
}
