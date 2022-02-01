
using System;
using System.Data.SqlClient;
using IposTransferData.Services;
using System.Threading.Tasks;
using IposTransferData.Model;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IposTransferData
{
    public class Program
    {
        static Program()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            Configuration = builder.Build();

            SqlConnection = new SqlConnection(Configuration.GetConnectionString("Source"));
            DestinationConnection = new SqlConnection(Configuration.GetConnectionString("Destination"));
        }

        public static SqlConnection SqlConnection { get; }
        public static SqlConnection DestinationConnection { get; }
        public static IConfiguration Configuration { get; private set; }
        public static ServiceProvider ServiceProvider { get; private set; }

        async static Task Main(string[] args)
        {
            GetServiceProvider();

            await ProcessProducts();
            await ProcessCategory();
        }

        public async static Task ProcessProducts()
        {
            var productService = ServiceProvider.GetService<IProductService>();
            var items = await productService.GetProducts();
            Console.WriteLine(items);
            //var product = items;
            var filtereditem = new List<Item>();

            foreach (var product in items)
            {
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
            var categoryService = ServiceProvider.GetService<ICategoryService>();
            var category = await categoryService.GetCategory();
            Console.WriteLine(category);

            var filteredcat = new List<Category>();
            foreach (var cat in category)
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
            var categoryitem = await categoryService.GetCategoryItem();
            var filteredcatitem = new List<CategoryItem>();
            foreach (var catitem in categoryitem)
            {
                await categoryService.InsertCategoryItem(catitem.Item_Id, catitem.Category_Id);
                filteredcatitem.Add((CategoryItem)catitem);
                var totalLIST = filteredcatitem.ToList();
                Console.WriteLine("Item Number:{0}", totalLIST.Count);
            }

        }

        private static void GetServiceProvider()
        {
            //setup our DI
            ServiceProvider = new ServiceCollection()
            .AddSingleton<ICategoryService>((provider) =>
            {
                var categoryService = new CategoryService(SqlConnection, DestinationConnection);
                return categoryService;
            })
            .AddSingleton<IProductService>((provider) =>
            {
                var productService = new ProductService(SqlConnection, DestinationConnection);
                return productService;

            }).BuildServiceProvider();

        }
    }
}
