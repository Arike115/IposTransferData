using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Categories
{
    public interface ICategoryItemService
    {
        Task<IEnumerable<CategoryItem>> GetCategoryItemsAsync();
        Task InsertCategoryItemAsync(CategoryItem categoryItem);
    }
}
