using NiceStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Payments.Domain
{
    public class OrderItem : Entity
    {       
        public Guid OrderId { get; private set; }   
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }
        //ef rel.
        public Order Order { get; private set; }    

        public OrderItem(Guid productId, string productName, int quantity, decimal unitValue)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitValue = unitValue;
        }

        protected OrderItem() { }   

        internal void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public decimal CalculateValue()
        {
            return Quantity * UnitValue;
        }   

        internal void AddUnit(int unit)
        {
            Quantity += unit;
        }   

        internal void UpdateUnit(int unit)
        {
            Quantity = unit;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
