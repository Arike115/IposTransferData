using IposTransferData.Dto;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IposTransferData.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategory();
        Task InsertCategoryData(Category cat);
        Task<IEnumerable<CategoryItemDto>> GetCategoryItem();
        Task InsertCategoryItem(Guid? item_Id, Guid? category_Id);
    }
}