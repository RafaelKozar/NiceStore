using NiceStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Payments.Domain
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public TypeDescountVoucher TypeDescountVoucher { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? UsedDate { get; private set; }
        public DateTime ValidityDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }
        
        // EF Rel.
        public ICollection<Order> Orders { get; set; }  
    }
}
