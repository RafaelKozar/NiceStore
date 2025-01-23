using NiceStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Domain.Events
{
    public class ProductBellowStockEvent : DomainEvent
    {
        public int RemainingStock { get; private set; }

        public ProductBellowStockEvent(Guid aggregateId, int remainingStock) : base(aggregateId)
        {
            RemainingStock = remainingStock;
        }
    }    
}
