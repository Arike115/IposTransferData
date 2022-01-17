using IposTransferData.core.Dto;
using IposTransferData.core.Model;
using IposTransferData.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.core.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IDataDapperService _db;
        public ProductService(IDataDapperService db)
        {
            _db = db;
        }

        public Task<IEnumerable<CategoryDto>> GetCategory()
        {
            var sql = @"select TOP 20 From Category ca
                        WHERE ca.IsDeleted <> 1
                        ORDER BY ca.MOdifiedOn DESC";

            var category = _db.GetData<CategoryDto, dynamic>(sql, null);
            return category;

        }

        public Task<IEnumerable<ProductDto>> GetProductsByCategoryId(int category_UId)
        {
            var sql = @"select TOP 20 From Product po
                        WHERE po.Category_UId = @category_UId";
            var Product = _db.GetData<ProductDto, dynamic>(sql, new { Category_UId = category_UId });
            return Product;

        }

        public Task<IEnumerable<ProductDto>> GetProducts()
        {
            var sql = @"select * From Product p
                        WHERE p.IsDeleted <> 1
                        ORDER BY p.ModifiedOnUtc DESC";

            var product = _db.GetData<ProductDto, dynamic>(sql, null);
            return product;

        }

        public Task InsertCategoryData(Category category)
        {
            var sql = @"INSERT into Category(Title,Description,ParentCategory,ParentCategoryId,IsDeleted,ModifiedOnUtc) Values
                        Name,Description,ParentCatId,ParentCatName,IsDeleted,ModifiedOn";

            var cat = _db.SaveData(sql, category);
            return cat;
        }

        public Task InsertProductData(Item product)
        {
            var productList = new List<Item>();
            var sql = @"INSERT into Item(Barcode,Quantity,Name,Description,ProductId,ProductUId,Price,CostPrice,PhotoUrl,FileName,FileSize,
                            IsDiscountable,IsDiscountinued,ReorderLevel,ExpiryDate,CreatedOn,ModifiedOn,IsDeleted,BatchNumber)Values
                            Barcode,Quantity,Title,Description,SellingPrice,CostPrice,LogoUrl,LogoOriginalFileName,
                            LogoFileSize,IsDiscountable,IsDiscontinue,ReorderLevel,ExpiryDate,CreatedOn,ModifiedOn FROM Product";

            var item = _db.SaveData(sql, product);
            return item;

        }
    }
}
