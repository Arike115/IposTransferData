using IposTransferData.core.Dto;
using IposTransferData.core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IposTransferData.core.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<CategoryDto>> GetCategory();
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<IEnumerable<ProductDto>> GetProductsByCategoryId(int category_UId);
        Task InsertCategoryData(Category category);
        Task InsertProductData(Item product);
    }
}