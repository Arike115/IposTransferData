using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Model
{
    public class Audit : BaseEntity
    {
        public Guid? PrimaryKey { get; set; }
        public string UserId { get; set; }
        public int ActionType { get; set; } = (int)AuditActions.DEFAULTS;
        public string Description { get; set; }
        public string HttpMethod { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string TableName { get; set; }
        public string IPAddress { get; set; }
        public string AreaAccessed { get; set; }
        public string TraceId { get; set; }
        public string BrowserInfo { get; set; }
        public string UserName { get; set; }
        public Guid? BusinessId { get; set; }
        public Guid? StoreId { get; set; }
    }

    public enum AuditActions
    {
        [Description("Unknown")]
        DEFAULTS,
        [Description("User mobile login")]
        LOGIN,
        [Description("User mobile login")]
        MOBILE_LOGIN,
        [Description("User web login at.")]
        WEB_LOGIN,
        [Description("Product created successfully")]
        NEW_PRODUCT,
        [Description("Product updated successfully")]
        EDIT_PRODUCT,
        [Description("Restock product")]
        RESTOCK_PRODUCT,
        [Description("Product deleted successfully")]
        DELETE_PRODUCT,
        [Description("Product added to waste")]
        ADD_PRODUCT_TO_SPOIL,
        [Description("Category created successfully.")]
        NEW_CATEGORY,
        [Description("Category deleted successfully.")]
        DELETE_CATEGORY,
        [Description("Category updated successfully.")]
        EDIT_CATEGORY,
        [Description("Create Supplier successfully.")]
        CREATE_SUPPLIER,
        [Description("Edit Supplier successfully")]
        EDIT_SUPPLIER,
        [Description("Supplier deleted successfully.")]
        DELETE_SUPPLIER,
        [Description("Token Refresh")]
        REFRESH_TOKEN,
        [Description("Customer created successfully")]
        NEW_CUSTOMER,
        [Description("Customer Edited successfully")]
        EDIT_CUSTOMER,
        [Description("Customer Deleted successfully")]
        DELETE_CUSTOMER,
        [Description("New Purchase Order Created")]
        NEW_PURCHASE_ORDER,
        [Description(" purchase order delivered")]
        EDIT_PURCHASE_ORDER,
        [Description("Delete Purchase Order Item")]
        DELETE_PURCHASEORDERITEM,
        [Description("Create Draft purchase Order")]
        NEW_DRAFT_PURCHASE_ORDER,
        [Description("Add Store")]
        NEW_STORE,
        [Description("Update Order")]
        UPDATE_ORDER,
        [Description("Open Order")]
        OPEN_ORDER,
        [Description("You successfully just completed a sale")]
        COMPLETE_ORDER,
        [Description("You just suspended an order with Id")]
        RECALL_ORDER,
        [Description("Order successfully recalled")]
        SUSPEND_ORDER,
        [Description("Tax created successfully")]
        CREATE_SETTINGS,
        [Description("Tax Edited successfully")]
        EDIT_SETTINGS,
        [Description("Tax Deleted successfully")]
        DELETE_SETTINGS,
        [Description("Create Role")]
        CREATE_ROLE,
        [Description("Edit Role")]
        EDIT_ROLE,
        [Description("Delete Role")]
        DELETE_ROLE,
        [Description("Delete User successfully")]
        DELETE_USER,
        [Description("Create User successfully")]
        CREATE_USER,
        [Description("Edit User successfully")]
        EDIT_USER,
        [Description("Create Bulk User")]
        CREATE_BULK_USER,
        [Description("Web Logout at")]
        WEB_LOGOUT,
        [Description("User Mobile Logout at")]
        LOGOUT,
        [Description("User Mobile Logout at")]
        MOBILE_LOGOUT,
        [Description("Check Store List")]
        GET_STORELIST,
        [Description("Check Customer Details")]
        GET_CUSTOMER_DETAILS,
        [Description("Check Store Customer Status")]
        GET_STORE_CUSTOMERSTAT,
        [Description("View SalesList")]
        VIEW_SALES_LIST,
        [Description("View Report Module")]
        VIEW_REPORT,
        [Description("View Sales Details")]
        VIEW_SALES,
        [Description("View Order Details")]
        VIEW_ORDER,
        [Description("View Stock Details")]
        VIEW_STOCK,
        [Description("View Spoil Details")]
        VIEW_SPOIL,
        [Description("View Order List")]
        VIEW_ORDER_LIST,
        [Description("View Spoil List")]
        VIEW_SPOIL_LIST,
        [Description("Delete sales")]
        VIEW_SAlES,
        [Description("Add product to favourite list")]
        ADD_ITEM_TO_FAVOURITE,
        [Description("Remove product from favourite list")]
        REMOVE_ITEM_FROM_fAVOURITE,
        [Description("Create Invoice")]
        CREATE_INVOICE,



    }
}
