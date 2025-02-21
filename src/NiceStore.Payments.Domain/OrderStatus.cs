using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Payments.Domain
{
    public enum OrderStatus
    {
        Draft = 0,
        Started = 1,
        Paid = 4,
        Delivered = 5,
        Canceled = 6
    }
}
