using IposTransferData.Dto;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IposTransferData.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<IEnumerable<ProductDto>> GetProductsByCategoryId(int category_UId);
        Task InsertProductData(Item Prod);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task InsertItemAsync(Item item);
    }
}