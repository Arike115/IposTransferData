using IposTransferData.Dto;
using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services
{
    public interface IsaleService
    {
        Task<IEnumerable<SaleDto>> GetSale();
        Task<IEnumerable<SaleDto>> GetSaleById(string saleid);
        Task InsertSaleData(Sale Prod);
        Task InsertPaymentDate(Payment pay);
    }
}
