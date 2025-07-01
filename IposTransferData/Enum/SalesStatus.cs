using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Enum
{
    public enum SalesStatus
    {
        [EnumMember(Value = "OPEN")]
        OPEN,
        [EnumMember(Value = "CANCELLED")]
        CANCELLED,
        [EnumMember(Value = "CLOSED")]
        CLOSED,
        [EnumMember(Value = "FULLY RECALLED")]
        FULLYRECALL,
        [EnumMember(Value = "PARTIALLY RECALLED")]
        PARTLYRECALL
    }
}
