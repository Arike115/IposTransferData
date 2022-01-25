
using IposTransferData.DataAccess;
using System;
using System.Data.SqlClient;
using IposTransferData.CategoryServices;
using IposTransferData.ProductServices;
using System.Threading.Tasks;
using IposTransferData.Model;
using System.Collections.Generic;
using System.Linq;

namespace IposTransferData
{
    public class Program
    {
        static Program()
        {
            connectionString = "Server=DESKTOP-M3U3Q02\\S_SQLEXPRESS;Database=Iposv3;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True";
            destinationString = "Server=DESKTOP-M3U3Q02\\S_SQLEXPRESS;Database=IposDataRetrieved;Trusted_Connection=true;MultipleActiveResultSets=false;TrustServerCertificate=True";
        }

        public static string connectionString;
        public static string destinationString;
        public static SqlConnection SqlConnection { get; set; } = new SqlConnection(connectionString);
        public static SqlConnection DestinationConnection { get; set; } = new SqlConnection(destinationString);
        async static Task Main(string[] args)
        {
           await ProcessProducts();
           await ProcessCategory();
        }

        public async static Task ProcessProducts()
        {
            IDataDapperService dataDapperService = new DataDapperService(SqlConnection, DestinationConnection);
            IProductService productService = new ProductService(dataDapperService);
            var items = await productService.GetProducts();
            Console.WriteLine(items);
            //var product = items;
            var filtereditem = new List<Item>();
            
            foreach(var product in items)
            {
                //if (items.Any())
                //{
                //   Console.WriteLine("Item name{0} already exists",product.Name);
                //    return;
                //}
                await productService.InsertProductData(
                   Guid.NewGuid(),
                    product.Barcode,
                    product.Quantity,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.CostPrice,
                    product.PhotoUrl,
                    product.FileName,
                    product.FileSize,
                    product.IsDiscountinued,
                    product.IsDiscountable,
                    product.ReorderLevel,
                    product.ModifiedOn ?? DateTime.Now,
                    product.CreatedOn = product.ModifiedOn ?? DateTime.Now,
                    product.IsDeleted,
                    null,
                    null,
                    product.Price,
                    null,
                    null);
                Console.WriteLine("Name:{0}", product.Name);
                filtereditem.Add((Item)product);
                var total = filtereditem.ToList();
                Console.WriteLine("Item Number:{0}", total.Count);

                

            }
            





        }


        public async static Task ProcessCategory()
        {
            IDataDapperService dataDapperService = new DataDapperService(SqlConnection, DestinationConnection);
            ICategoryService categoryService = new CategoryService(dataDapperService);
            var category = await categoryService.GetCategory();
            Console.WriteLine(category);

            var filteredcat = new List<Category>();
            foreach ( var cat in category)
            {
                await categoryService.InsertCategoryData(
                  Guid.NewGuid(),
                  cat.Name,
                  cat.Description,
                  null,
                  cat.IsDeleted,
                  cat.ModifiedOn ?? DateTime.Now,
                  cat.CreatedOn = cat.ModifiedOn ?? DateTime.Now,
                  null);
                Console.WriteLine("Name:{0}", cat.Name);
                filteredcat.Add((Category)cat);
                var total = filteredcat.ToList();
                Console.WriteLine("Item Number:{0}", total.Count);


            }
        }


    }
}
