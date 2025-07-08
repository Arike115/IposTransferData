using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IposTransferData.Model;

namespace IposTransferData.Services.SaleItemTaxes
{
    public interface ISaleItemTaxService
    {
        Task<IEnumerable<Model.SaleItemTax>> GetSaleItemTaxesAsync();
        Task InsertSaleItemTaxAsync(Model.SaleItemTax saleItemTax);
    }
}
