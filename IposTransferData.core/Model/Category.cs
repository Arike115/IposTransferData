using IposTransferData.core.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.core.Model
{
    public class Category
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string LogoName { get; set; }
        public long LogoFileSize { get; set; }
        public string LogoOriginalFileName { get; set; }
        public long ItemCount { get; set; }
        public string ParentCategory { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string FileBytes { get; set; }
        public IFormFile File { get; set; }
        public string Logo { get; set; }
        public string LogoContentType { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public static explicit operator Category(CategoryDto source)
        {
            var destination = new Category();
            destination.Title = source.Name;
            destination.Description = source.Description;
            destination.ParentCategory = source.ParentCatName;
            destination.ParentCategoryId = source.ParentCatId;
            destination.ModifiedOn = source.ModifiedOnUtc;


            return destination;
        }
    }
}
