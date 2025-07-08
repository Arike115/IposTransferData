using IposTransferData.Dto;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IposTransferData.Services.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task InsertCategoryAsync(Category category);
    }
}