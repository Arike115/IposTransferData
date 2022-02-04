
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
using System.Threading;

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
            Console.WriteLine("About to process data");
            await ProcessData();
            Console.WriteLine("Done with the processing of data");
            
        }


        public async static Task ProcessData()
        {
            var categoryService = ServiceProvider.GetService<ICategoryService>();
            var productService = ServiceProvider.GetService<IProductService>();

            Console.WriteLine("About to process the categories");
            Console.WriteLine();

            var categories = await categoryService.GetCategory();

            Console.WriteLine("The total number of categories is => " + categories.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the categories");

            foreach (var cat in categories)
            {
                Console.WriteLine("Currently processing the category whose name is ==>  " + cat.Name);

                var category = new Category()
                {
                    Id = Guid.NewGuid(),
                    Title = cat.Name,
                    Description = cat.Description,
                    IsDeleted = cat.IsDeleted,
                    ParentCategory_Id = null,
                    LogoFileSize = null,
                    ModifiedOn = cat.ModifiedOn ?? DateTime.Now,
                    CreatedOn = cat.CreatedOn ?? DateTime.Now,
                };

                Console.WriteLine("About saving the category whose name is ==> " + cat.Name);

                await categoryService.InsertCategoryData(category);

                Console.WriteLine("Successfully saved the category whose name is ==> " + cat.Name);
                Console.WriteLine("About fetching the list of products under the category whose name is ==> " + cat.Name);
                
                var products = await productService.GetProductsByCategoryId(cat.CategoryUId);

                Console.WriteLine("The total number of products under the category {0} is {1}", cat.Name, products.Count());
                Console.WriteLine("Currently looping through the products under the category whose name is " + cat.Name);

                foreach (var product in products)
                {
                    Console.WriteLine("Currently processing the product whose name is ==>  " + product.Name);
                    Console.WriteLine("About saving the product whose name is ==> " + product.Name);

                    var prod = new Item()
                    {
                        Id = product.ProductUId,
                        Barcode = product.Barcode,
                        Quantity = product.Quantity,
                        Title = product.Name,
                        Description = product.Description,
                        SellingCost = product.Price,
                        ActualCost = product.CostPrice,
                        LogoUrl = product.PhotoUrl,
                        LogoOriginalFileName = product.FileName,
                        LogoFileSize = product.FileSize,
                        IsDiscontinue = product.IsDiscountinued,
                        IsDiscountable = product.IsDiscountable,
                        ReorderLevel = product.ReorderLevel,
                        ModifiedOn = product.ModifiedOn ?? DateTime.Now,
                        CreatedOn = product.CreatedOn = product.ModifiedOn ?? DateTime.Now,
                        IsDeleted = product.IsDeleted,
                        Weight = null,
                        ItemsType = null,
                        PreviousSellingCost = product.Price,
                        ExtraCharge = null,
                        DiscountLimit = null,
                    }; 

                    await productService.InsertProductData(prod);

                    Console.WriteLine("Successfully saved the product whose name is " + product.Name);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("About saving the product guid and category guid for product whose guid is {0} and category guid is {1} into the category Item table", product.ProductUId, category.Id);

                    await categoryService.InsertCategoryItem(product.ProductUId, category.Id);

                    Console.WriteLine("Successfully saved the product guid and category guid for product whose guid is {0} and category guid is {1} into the category Item table", product.ProductUId, category.Id);
                }
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
