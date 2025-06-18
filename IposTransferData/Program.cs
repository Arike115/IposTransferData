
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
using IposTransferData.Enum;

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
            var saleService = ServiceProvider.GetService<IsaleService>();


            Console.WriteLine("About to process the categories");
            Console.WriteLine();

            var sale = await saleService.GetSale();

            Console.WriteLine("The total number of categories is => " + sale.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the categories");

            foreach (var data in sale)
            {
                Console.WriteLine("Currently processing the category whose name is ==>  " + data.PaymentType);

                var saledata = new Sale()
                {
                    Id = Guid.NewGuid(),
                    RefNo = data.RefNo,
                    NetPrice = data.NetCost,
                    Cost = data.Cost,
                    NetCost = data.NetPrice,
                    Discount = data.Discount,
                    NetItemDiscount = data.NetItemDiscount,
                    Tax = data.Tax,
                    SumQuantity = data.SumQuantity,
                    ExtraCharges = data.ExtraCharges,
                    Status = data.Status,
                    CustomerDetail = null,
                    Customer_Id = null,
                    PaymentCategory = PaymentCategory.SINGLEPAYEMENT,
                    Business_Id = data.Business_Id,
                    Store_Id = data.Store_Id,
                    StoreDetail = data.StoreDetail,
                    Remarks = data.Remarks,
                    TransactionDate = data.TransactionDate,
                    ValueDate = data.ValueDate,
                    DueDate = data.DueDate,
                    AmountTender = data.AmountTender,
                    CurrencyCode = data.CurrencyCode,
                    IsDeleted = data.IsDeleted,
                    CreatedBy = data.CreatedBy,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedOn = data.ModifiedOn ?? DateTime.Now,
                    CreatedOn = data.CreatedOn ?? DateTime.Now,
                };

                Console.WriteLine("About saving the category whose name is ==> " + data.AmountTender);

                await saleService.InsertSaleData(saledata);

                Console.WriteLine("Successfully saved the category whose name is ==> " + data.AmountTender);
                Console.WriteLine("About fetching the list of products under the category whose name is ==> " + data.AmountTender);
                
                var products = await saleService.GetSaleById(data.Id.ToString());

               
                    Console.WriteLine("Currently processing the product whose name is ==>  " + data.PaymentType);
                    Console.WriteLine("About saving the product whose name is ==> " + data.PaymentType);

                    var prod = new Payment()
                    {
                        Id = Guid.NewGuid(),
                        Sale_Id = saledata.Id,
                        PaymentType = data.PaymentType,
                        PaymentAmount = (double)data.NetCost,
                        PaymentCategory = PaymentCategory.SINGLEPAYEMENT,
                        IsDeleted = data.IsDeleted,
                        CreatedBy = data.CreatedBy,
                        ModifiedBy = data.ModifiedBy,
                        ModifiedOn = data.ModifiedOn, 
                        CreatedOn = data.CreatedOn,
                    }; 

                    await saleService.InsertPaymentDate(prod);

                    Console.WriteLine("Successfully saved the product whose name is " + data.AmountTender);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Successfully saved the product");
                
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
             .AddSingleton<IsaleService>((provider) =>
             {
                 var saleService = new SaleService(SqlConnection, DestinationConnection);
                 return saleService;
             })
            .AddSingleton<IProductService>((provider) =>
            {
                var productService = new ProductService(SqlConnection, DestinationConnection);
                return productService;

            }).BuildServiceProvider();

        }
    }
}
