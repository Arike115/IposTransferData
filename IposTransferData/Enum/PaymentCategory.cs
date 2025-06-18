using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Enum
{

    public enum PaymentTypes
    {
        CASH,
        TRANSFER,
        POS,
        WALLET,
    }
    public enum PaymentCategory
    {
        SINGLEPAYEMENT = 1,
        MULTIPLEPAYMENT = 2
    }


    public static class PaymentTypeHelper
    {
        public static PaymentTypes ConvertStringToTypes(this string type)
        {
            return type switch
            {
                "CASH" => PaymentTypes.CASH,
                "POS" => PaymentTypes.POS,
                "TRANSFER" => PaymentTypes.TRANSFER,
                "WALLET" => PaymentTypes.WALLET,
                _ => throw new Exception("Invalid Paymemt Types")
            };
        }
    }
}

