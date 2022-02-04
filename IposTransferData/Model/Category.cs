using IposTransferData.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Category
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ParentCategory { get; set; }
        public Guid? ParentCategory_Id { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long? LogoFileSize { get; set; }

        public static explicit operator Category(CategoryDto source)
        {
            var destination = new Category();
            destination.Id = new Guid();
            destination.Title = source.Name;
            destination.Description = source.Description;
            destination.ParentCategory = source.ParentCatName;
            //destination.ParentCategory_Id = source.ParentCatId;
            destination.ModifiedOn = source.ModifiedOn;
            destination.CreatedOn = source.ModifiedOn;
            destination.IsDeleted = source.IsDeleted;


            return destination;
        }
    }

    public class CategoryItem
    {

        public Guid? Item_Id { get; set; }
        public Guid? Category_Id { get; set; }

        public static explicit operator CategoryItem(CategoryItemDto v)
        {
            var destination = new CategoryItem();
            //destination.Category_Id = new Guid();
            destination.Item_Id = v.ProductUId;
            return destination;
        }




    } }
