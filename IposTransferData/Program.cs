
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
using IposTransferData.Services.IposUsers;
using IposTransferData.Services.Businesses;
using IposTransferData.Services.Stores;
using IposTransferData.Constants;
using IposTransferData.Services.IposRoles;
using System.Configuration;
using IposTransferData.Services.Settings;
using IposTransferData.Services.Categories;
using IposTransferData.Services.ItemBatches;
using IposTransferData.Services.Suppliers;
using IposTransferData.Services.PurchaseOrders;
using IposTransferData.Services.Clients;
using IposTransferData.Services.Wallets;
using IposTransferData.Services.Customers;
using IposTransferData.Services.Spoils;
using IposTransferData.Services.Audits;
using IposTransferData.Services.PendingSales;
using IposTransferData.Services.SaleItems;
using IposTransferData.Services.SaleItemTaxes;
using IposTransferData.Services.ReferralHistories;

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
            var iposUserService = ServiceProvider.GetService<IIposUserService>();
            var businessService = ServiceProvider.GetService<IBusinessService>();
            var storeService = ServiceProvider.GetService<IStoreService>();
            var iposRoleService = ServiceProvider.GetService<IIposRoleService>();
            var iposUserRoleService = ServiceProvider.GetService<IIposUserRoleService>();
            var settingsService = ServiceProvider.GetService<ISettingsService>();
            var categoryItemService = ServiceProvider.GetService<ICategoryItemService>();
            var itemBatchService = ServiceProvider.GetService<IItemBatchService>();
            var supplierService = ServiceProvider.GetService<ISupplierService>();
            var purchaseOrderService = ServiceProvider.GetService<IPurchaseOrderService>();
            var purchaseOrderItemService = ServiceProvider.GetService<IPurchaseOrderItemService>();
            var itemTaxService = ServiceProvider.GetService<IItemTaxService>();
            var clientService = ServiceProvider.GetService<IClientService>();
            var storeAccountService = ServiceProvider.GetService<IStoreAccountService>();
            var storeNAccountService = ServiceProvider.GetService<IStoreNAccountService>();
            var walletService = ServiceProvider.GetService<IWalletService>();
            var walletHistoryService = ServiceProvider.GetService<IWalletHistoryService>();
            var customerService = ServiceProvider.GetService<ICustomerService>();
            var spoilService = ServiceProvider.GetService<ISpoilService>();
            var auditService = ServiceProvider.GetService<IAuditService>();
            var pendingSalesService = ServiceProvider.GetService<IPendingSaleService>();
            var saleItemService = ServiceProvider.GetService<ISaleItemService>();
            var saleItemTaxService = ServiceProvider.GetService<ISaleItemTaxService>();
            var referralHistoryService = ServiceProvider.GetService<IReferralHistoryService>();

            await MigrateBusinesses(businessService);

            await MigrateStores(storeService);

            await MigrateIposUsers(iposUserService, storeService);

            await MigrateIposRoles(iposRoleService);

            await MigrateIposUserRoles(iposUserRoleService);

            await MigrateSettings(settingsService);

            await MigrateCategories(categoryService);

            await MigrateItems(productService);

            await MigrateCategoryItems(categoryItemService);

            await MigrateSuppliers(supplierService);

            await MigratePurchaseOrders(purchaseOrderService);

            await MigratePurchaseOrerItems(purchaseOrderItemService);

            await MigrateItemBatches(itemBatchService);

            await MigrateItemTaxes(itemTaxService, itemBatchService);

            await MigrateClients(clientService);

            await MigrateStoreAccounts(storeAccountService);

            await MigrateStoreNAccounts(storeNAccountService);

            await MigrateWallets(walletService);

            await MigrateWalletHistories(walletHistoryService);

            await MigrateCustomers(customerService);

            await MigrateSpoils(spoilService);

            await MigrateAudits(auditService);

            await MigrateSales(saleService);

            await MigratePendingSales(pendingSalesService);

            await MigrateSaleItems(saleItemService);

            await MigrateSaleItemTaxes(saleItemTaxService);

            await MigrateReferralHistories(referralHistoryService);
        }

        private static async Task MigrateItems(IProductService itemService)
        {
            Console.WriteLine("About to process the items");
            Console.WriteLine();

            var items = await itemService.GetItemsAsync();

            Console.WriteLine("The total number of items is => " + items.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the items");

            foreach (var item in items)
            {
                await itemService.InsertItemAsync(item);
            }
            Console.WriteLine("Ended processing items");
            Console.WriteLine();
        }

        private static async Task MigrateClients(IClientService clientService)
        {
            Console.WriteLine("About to process the clients");
            Console.WriteLine();

            var clients = await clientService.GetClientsAsync();

            Console.WriteLine("The total number of clients is => " + clients.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the clients");

            foreach (var item in clients)
            {
                await clientService.InsertClientAsync(item);
            }
            Console.WriteLine("Ended processing clients");
            Console.WriteLine();
        }

        private static async Task MigrateCustomers(ICustomerService customerService)
        {
            Console.WriteLine("About to process the customers");
            Console.WriteLine();

            var customers = await customerService.GetCustomersAsync();

            Console.WriteLine("The total number of customers is => " + customers.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the customers");

            foreach (var item in customers)
            {
                await customerService.InsertCustomerAsync(item);
            }
            Console.WriteLine("Ended processing customers");
            Console.WriteLine();
        }

        private static async Task MigrateReferralHistories(IReferralHistoryService referralHistoryService)
        {
            Console.WriteLine("About to process the referral histories");
            Console.WriteLine();

            var referralHistories = await referralHistoryService.GetReferralHistoriesAsync();

            Console.WriteLine("The total number of referral histories is => " + referralHistories.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the referral histories");

            foreach (var item in referralHistories)
            {
                await referralHistoryService.InsertReferralHistoryAsync(item);
            }
            Console.WriteLine("Ended processing referral histories");
            Console.WriteLine();
        }

        private static async Task MigrateSaleItems(ISaleItemService saleItemService)
        {
            Console.WriteLine("About to process the sale items");
            Console.WriteLine();

            var saleItems = await saleItemService.GetSaleItemsAsync();

            Console.WriteLine("The total number of sale items is => " + saleItems.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the sale items");

            foreach (var item in saleItems)
            {
                await saleItemService.InsertSaleItemAsync(item);
            }
            Console.WriteLine("Ended processing sale items");
            Console.WriteLine();
        }

        private static async Task MigrateSaleItemTaxes(ISaleItemTaxService saleItemService)
        {
            Console.WriteLine("About to process the sale item taxes");
            Console.WriteLine();

            var saleItemTaxes = await saleItemService.GetSaleItemTaxesAsync();

            Console.WriteLine("The total number of sale item taxes is => " + saleItemTaxes.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the sale item taxes");

            foreach (var item in saleItemTaxes)
            {
                await saleItemService.InsertSaleItemTaxAsync(item);
            }
            Console.WriteLine("Ended processing sale item taxess");
            Console.WriteLine();
        }

        private static async Task MigratePendingSales(IPendingSaleService pendingSalesService)
        {
            Console.WriteLine("About to process the pending sales");
            Console.WriteLine();

            var pendingSales = await pendingSalesService.GetPendingSalesAsync();

            Console.WriteLine("The total number of pending sales is => " + pendingSales.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the pending sales");

            foreach (var item in pendingSales)
            {
                await pendingSalesService.InsertPendingSaleAsync(item);
            }
            Console.WriteLine("Ended processing pending sales");
            Console.WriteLine();
        }

        private static async Task MigrateSpoils(ISpoilService spoilService)
        {
            Console.WriteLine("About to process the spoils");
            Console.WriteLine();

            var spoils = await spoilService.GetSpoilsAsync();

            Console.WriteLine("The total number of spoils is => " + spoils.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the spoils");

            foreach (var item in spoils)
            {
                await spoilService.InsertSpoilAsync(item);
            }
            Console.WriteLine("Ended processing spoils");
            Console.WriteLine();
        }

        private static async Task MigrateAudits(IAuditService spoilService)
        {
            Console.WriteLine("About to process the audits");
            Console.WriteLine();

            var audits = await spoilService.GetAuditsAsync();

            Console.WriteLine("The total number of audits is => " + audits.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the audits");

            foreach (var item in audits)
            {
                await spoilService.InsertAuditAsync(item);
            }
            Console.WriteLine("Ended processing audits");
            Console.WriteLine();
        }

        private static async Task MigrateWalletHistories(IWalletHistoryService walletService)
        {
            Console.WriteLine("About to process the wallet histories");
            Console.WriteLine();

            var wallets = await walletService.GetWalletHistoriesAsync();

            Console.WriteLine("The total number of wallet histories is => " + wallets.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the wallet histories");

            foreach (var item in wallets)
            {
                await walletService.InsertWalletHistoryAsync(item);
            }
            Console.WriteLine("Ended processing wallet histories");
            Console.WriteLine();
        }

        private static async Task MigrateWallets(IWalletService walletService)
        {
            Console.WriteLine("About to process the wallets");
            Console.WriteLine();

            var wallets = await walletService.GetWalletsAsync();

            Console.WriteLine("The total number of wallets is => " + wallets.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the wallets");

            foreach (var item in wallets)
            {
                await walletService.InsertWalletAsync(item);
            }
            Console.WriteLine("Ended processing wallets");
            Console.WriteLine();
        }

        private static async Task MigrateStoreNAccounts(IStoreNAccountService storeAccountService)
        {
            Console.WriteLine("About to process the store accounts");
            Console.WriteLine();

            var storeAccounts = await storeAccountService.GetStoreNAccountsAsync();

            Console.WriteLine("The total number of store accounts is => " + storeAccounts.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the store accounts");

            foreach (var item in storeAccounts)
            {
                await storeAccountService.InsertStoreNAccountAsync(item);
            }
            Console.WriteLine("Ended processing store accounts");
            Console.WriteLine();
        }

        private static async Task MigrateStoreAccounts(IStoreAccountService storeAccountService)
        {
            Console.WriteLine("About to process the store accounts");
            Console.WriteLine();

            var storeAccounts = await storeAccountService.GetStoreAccountsAsync();

            Console.WriteLine("The total number of store accounts is => " + storeAccounts.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the store accounts");

            foreach (var item in storeAccounts)
            {
                await storeAccountService.InsertStoreAccountAsync(item);
            }
            Console.WriteLine("Ended processing store accounts");
            Console.WriteLine();
        }

        private static async Task MigrateItemTaxes(IItemTaxService itemTaxService, IItemBatchService itemBatchService)
        {
            Console.WriteLine("About to process the item taxes");
            Console.WriteLine();

            var itemTaxes = await itemTaxService.GetItemTaxesAsync();

            Console.WriteLine("The total number of item taxes is => " + itemTaxes.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the item taxes");

            foreach (var item in itemTaxes)
            {
                var activeBatch = await itemBatchService.GetActiveItemBatchByItemAsync(item.Item_Id.Value);
                item.ItemBatch_Id = activeBatch.Id;
                await itemTaxService.InsertItemTaxAsync(item);
            }
            Console.WriteLine("Ended processing items");
            Console.WriteLine();
        }

        private static async Task MigratePurchaseOrerItems(IPurchaseOrderItemService itemService)
        {
            Console.WriteLine("About to process the purchase order items");
            Console.WriteLine();

            var items = await itemService.GetPurchaseOrderItemsAsync();

            Console.WriteLine("The total number of  purchase order items is => " + items.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the  purchase order items");

            foreach (var item in items)
            {
                await itemService.InsertPurchaseOrderItemAsync(item);
            }
            Console.WriteLine("Ended processing  purchase order items");
            Console.WriteLine();
        }

        private static async Task MigrateSuppliers(ISupplierService supplierService)
        {
            Console.WriteLine("About to process the suppliers");
            Console.WriteLine();

            var suppliers = await supplierService.GetSuppliersAsync();

            Console.WriteLine("The total number of suppliers is => " + suppliers.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the suppliers");

            foreach (var item in suppliers)
            {
                if (item.Address.Length > 100)
                {
                    item.Address = item.Address.Substring(0, 100);
                }
                await supplierService.InsertSupplierAsync(item);
            }
            Console.WriteLine("Ended processing suppliers");
            Console.WriteLine();
        }

        private static async Task MigratePurchaseOrders(IPurchaseOrderService purchaseOrderService)
        {
            Console.WriteLine("About to process the purchase orders");
            Console.WriteLine();

            var purchaseOrders = await purchaseOrderService.GetPurchaseOrdersAsync();

            Console.WriteLine("The total number of purchase orders is => " + purchaseOrders.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the purchase orders");

            foreach (var item in purchaseOrders)
            {
                await purchaseOrderService.InsertPurchaseOrderAsync(item);
            }
            Console.WriteLine("Ended processing purchase orders");
            Console.WriteLine();
        }

        private static async Task MigrateItemBatches(IItemBatchService itemService)
        {
            Console.WriteLine("About to process the item batches");
            Console.WriteLine();

            var items = await itemService.GetItemBatchesAsync();

            Console.WriteLine("The total number of item batches is => " + items.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the item batches");

            foreach (var item in items)
            {
                var lastBatch = await itemService.GetItemBatchByItemAsync(item.Item_Id);
                if (lastBatch.Id == item.Id)
                {
                    item.BatchStatus = ItemBatchStatuses.ACTIVE;
                }
                await itemService.InsertItemBatchAsync(item);
            }

            Console.WriteLine("Ended processing item batches");
            Console.WriteLine();
        }

        private static async Task MigrateSales(IsaleService saleService)
        {
            Console.WriteLine("About to process the sales");
            Console.WriteLine();

            var sale = await saleService.GetSale();

            Console.WriteLine("The total number of sales is => " + sale.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the sales");

            foreach (var data in sale)
            {
               // Console.WriteLine("Currently processing the sales whose Id is ==>  " + data.Id);

                var saledata = new Sale()
                {
                    Id = (Guid)data.Id,
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
                    CustomerDetail = data.CustomerDetail,
                    Customer_Id = data.Customer_Id,
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

              //  Console.WriteLine("About saving the sale whose Id is ==> " + data.AmountTender);

                await saleService.InsertSaleData(saledata);

                if (data.Status == 2)
                {
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
                        CreatedOn = data.CreatedOn
                    };

                    await saleService.InsertPaymentDate(prod);
                }
            }
            Console.WriteLine("Successfully saved the Sales details");
        }

        private static async Task MigrateBusinesses(IBusinessService businessService)
        {
            Console.WriteLine("About to process the Business");
            Console.WriteLine();

            var businesses = await businessService.GetBusinessesAsync();

            Console.WriteLine("The total number of businesses is => " + businesses.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the businesses");

            foreach (var business in businesses)
            {
                await businessService.InsertBusinessAsync(business);
            }
            Console.WriteLine("Ended processing Businesses");
            Console.WriteLine();
        }

        private static async Task MigrateSettings(ISettingsService settingsService)
        {
            Console.WriteLine("About to process the settings");
            Console.WriteLine();

            var businesses = await settingsService.GetSettingsAsync();

            Console.WriteLine("The total number of settings is => " + businesses.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the settings");

            foreach (var business in businesses)
            {
                await settingsService.InsertSettingsAsync(business);
            }
            Console.WriteLine("Ended processing settings");
            Console.WriteLine();
        }

        private static async Task MigrateStores(IStoreService storeService)
        {
            Console.WriteLine("About to process the stores");
            Console.WriteLine();

            var stores = await storeService.GetStoresAsync();

            Console.WriteLine("The total number of stores is => " + stores.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the stores");

            foreach (var store in stores)
            {
                await storeService.InsertStoreAsync(store);
            }
            Console.WriteLine("Ended processing stores");
            Console.WriteLine();
        }

        private static async Task MigrateIposRoles(IIposRoleService iposRoleService)
        {
            Console.WriteLine("About to process the roled");
            Console.WriteLine();

            var stores = await iposRoleService.GetIposRolesAsync();

            Console.WriteLine("The total number of roles is => " + stores.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the roles");

            foreach (var store in stores)
            {
                await iposRoleService.InsertIposRoleAsync(store);
            }
            Console.WriteLine("Ended processing roles");
            Console.WriteLine();
        }

        private static async Task MigrateIposUserRoles(IIposUserRoleService iposUserRoleService)
        {
            Console.WriteLine("About to process the user roled");
            Console.WriteLine();

            var stores = await iposUserRoleService.GetUserRolesAsync();

            Console.WriteLine("The total number of user roles is => " + stores.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the user roles");

            foreach (var store in stores)
            {
                await iposUserRoleService.InsertUserRoleAsync(store);
            }
            Console.WriteLine("Ended processing user roles");
            Console.WriteLine();
        }

        private static async Task MigrateIposUsers(IIposUserService iposUserService, IStoreService storeService)
        {
            Console.WriteLine("About to process the IposUsers");
            Console.WriteLine();

            var users = await iposUserService.GetUsersAsync();

            Console.WriteLine("The total number of users is => " + users.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the users");

            foreach (var user in users)
            {
                if (string.IsNullOrWhiteSpace(user.UserName))
                if (await iposUserService.UserExists(user.UserName))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                    if (await iposUserService.UserExists(user.Email))
                    {
                        continue;
                    }

                if (string.IsNullOrWhiteSpace(user.TimeZone))
                    user.TimeZone = LogicConstants.TimeZoneStandardName;

                if(user.Activated && user.Business_Id is not null)
                {
                    user.RegState = 4;
                }
                else if(!user.Activated && user.Business_Id is not null)
                {
                    user.RegState = 4;
                }
                else
                {
                    user.RegState = 2;
                }
                if (user.Business_Id is not null && user.Store_Id is null)
                {
                    var store = await storeService.GetStoreByBusiness((Guid)user.Business_Id);
                    user.Store_Id = store?.Id;
                }

                if (user.PhoneNumber.StartsWith("deleted"))
                {
                    var phoneNumber = user.PhoneNumber.Split('-');
                    user.PhoneNumber = phoneNumber[1];
                }
                await iposUserService.InsertUserAsync(user);
            }
            Console.WriteLine("Ended processing Ipos users");
            Console.WriteLine();
        }

        private static async Task MigrateCategories(ICategoryService categoryService)
        {
            Console.WriteLine("About to process the categories");
            Console.WriteLine();

            var categories = await categoryService.GetCategoriesAsync();

            Console.WriteLine("The total number of categories is => " + categories.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the categories");

            foreach (var category in categories)
            {
                await categoryService.InsertCategoryAsync(category);
            }
            Console.WriteLine("Ended processing categories");
            Console.WriteLine();
        }

        private static async Task MigrateCategoryItems(ICategoryItemService categoryService)
        {
            Console.WriteLine("About to process the category items");
            Console.WriteLine();

            var categories = await categoryService.GetCategoryItemsAsync();

            Console.WriteLine("The total number of category items is => " + categories.Count());
            Console.WriteLine();
            Console.WriteLine("Currently looping through the category items");

            foreach (var category in categories)
            {
                await categoryService.InsertCategoryItemAsync(category);
            }
            Console.WriteLine("Ended processing category items");
            Console.WriteLine();
        }

        private static void GetServiceProvider()
        {
            //setup our DI
            ServiceProvider = new ServiceCollection()
            .AddSingleton<IReferralHistoryService>((provider) =>
            {
                var referralHistoryService = new ReferralHistoryService(SqlConnection, DestinationConnection);
                return referralHistoryService;
            })
            .AddSingleton<ISaleItemTaxService>((provider) =>
            {
                var saleItemTaxService = new SaleItemTaxService(SqlConnection, DestinationConnection);
                return saleItemTaxService;
            })
            .AddSingleton<ISaleItemService>((provider) =>
            {
                var saleItemService = new SaleItemService(SqlConnection, DestinationConnection);
                return saleItemService;
            })
            .AddSingleton<IPendingSaleService>((provider) =>
            {
                var pendingSaleService = new PendingSaleService(SqlConnection, DestinationConnection);
                return pendingSaleService;
            })
            .AddSingleton<IAuditService>((provider) =>
            {
                var auditService = new AuditService(SqlConnection, DestinationConnection);
                return auditService;
            })
            .AddSingleton<ISpoilService>((provider) =>
            {
                var spoilService = new SpoilService(SqlConnection, DestinationConnection);
                return spoilService;
            })
            .AddSingleton<ICustomerService>((provider) =>
            {
                var customerService = new CustomerService(SqlConnection, DestinationConnection);
                return customerService;
            })
            .AddSingleton<IWalletHistoryService>((provider) =>
            {
                var walletHistoryService = new WalletHistoryService(SqlConnection, DestinationConnection);
                return walletHistoryService;
            })
            .AddSingleton<IWalletService>((provider) =>
            {
                var walletService = new WalletService(SqlConnection, DestinationConnection);
                return walletService;
            })
            .AddSingleton<IStoreNAccountService>((provider) =>
            {
                var storeNAccountService = new StoreNAccountService(SqlConnection, DestinationConnection);
                return storeNAccountService;
            })
            .AddSingleton<IStoreAccountService>((provider) =>
            {
                var storeAccountService = new StoreAccountService(SqlConnection, DestinationConnection);
                return storeAccountService;
            })
            .AddSingleton<IClientService>((provider) =>
            {
                var clientService = new ClientService(SqlConnection, DestinationConnection);
                return clientService;
            })
            .AddSingleton<IItemTaxService>((provider) =>
            {
                var itemTaxService = new ItemTaxService(SqlConnection, DestinationConnection);
                return itemTaxService;
            })
            .AddSingleton<IPurchaseOrderItemService>((provider) =>
            {
                var purchaseOrderItemService = new PurchaseOrderItemService(SqlConnection, DestinationConnection);
                return purchaseOrderItemService;
            })
            .AddSingleton<IPurchaseOrderService>((provider) =>
            {
                var purchaseOrderService = new PurchaseOrderService(SqlConnection, DestinationConnection);
                return purchaseOrderService;
            })
            .AddSingleton<ISupplierService>((provider) =>
            {
                var supplierService = new SupplierService(SqlConnection, DestinationConnection);
                return supplierService;
            })
            .AddSingleton<IItemBatchService>((provider) =>
            {
                var itemBatchService = new ItemBatchService(SqlConnection, DestinationConnection);
                return itemBatchService;
            })
            .AddSingleton<ICategoryItemService>((provider) =>
            {
                var categoryItemService = new CategoryItemService(SqlConnection, DestinationConnection);
                return categoryItemService;
            })
            .AddSingleton<ICategoryService>((provider) =>
            {
                var categoryService = new CategoryService(SqlConnection, DestinationConnection);
                return categoryService;
            })
            .AddSingleton<ISettingsService>((provider) =>
            {
                var settingsService = new SettingsService(SqlConnection, DestinationConnection);
                return settingsService;
            })
            .AddSingleton<IIposUserRoleService>((provider) =>
            {
                var iposUserRoleService = new IposUserRoleService(SqlConnection, DestinationConnection);
                return iposUserRoleService;
            })
            .AddSingleton<IIposRoleService>((provider) =>
            {
                var iposRoleService = new IposRoleService(SqlConnection, DestinationConnection);
                return iposRoleService;
            })
            .AddSingleton<IStoreService>((provider) =>
            {
                var storeService = new StoreService(SqlConnection, DestinationConnection);
                return storeService;
            })
            .AddSingleton<IBusinessService>((provider) =>
            {
                var businessService = new BusinessService(SqlConnection, DestinationConnection);
                return businessService;
            })
            .AddSingleton<IIposUserService>((provider) =>
            {
                var iposUserService = new IposUserService(SqlConnection, DestinationConnection);
                return iposUserService;
            })
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
