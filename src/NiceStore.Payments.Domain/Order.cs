using NiceStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Payments.Domain
{
    public class Order : Entity, IAggregateRoot
    {        
        public int Code { get; private set; }   
        public Guid ClientId { get; private set; }
        public Guid VoucherId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime DataRegister { get; private set; }
        public OrderStatus OrderStatus { get; private set; }        
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        // EF Rel.
        public virtual Voucher Voucher { get; private set; }
        
        public Order(Guid clientId, bool voucherUsed, decimal discount, decimal totalValue)
        {
            ClientId = clientId;
            VoucherUsed = voucherUsed;
            Discount = discount;
            TotalValue = totalValue;
            _orderItems = new List<OrderItem>();
        }

        protected Order() 
        { 
            _orderItems = new List<OrderItem>();
        }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUsed = true;
            CaculateValueOrder();
        }   

        public void CaculateValueOrder()
        {
            TotalValue = OrderItems.Sum(p => p.CalculateValue());
            CaculateTotalValueDiscount();
        }

        public void CaculateTotalValueDiscount()
        {
            if(!VoucherUsed) return;

            decimal discount = 0;
            var price = TotalValue; 

            if(Voucher.TypeDescountVoucher == TypeDescountVoucher.Percentage)
            {
                if(Voucher.Percentage.HasValue)
                {
                    discount = (price * Voucher.Percentage.Value) / 100;
                    price -= discount;
                }
            }
            else
            {
                if(Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    price -= discount;
                }
            }

            TotalValue = price < 0 ? 0 : price; 
            Discount = discount;
        }

        public bool OrderItemExists(OrderItem orderItem)
        {
            return _orderItems.Any(p => p.ProductId == orderItem.ProductId);
        }

        public void AddItem(OrderItem orderItem)
        {
            if(!orderItem.IsValid()) return;

           
            if(OrderItemExists(orderItem))
            {
                var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId);
                existingItem.AddUnit(orderItem.Quantity);
                orderItem = existingItem;

                _orderItems.Remove(existingItem);   
            }
            //rem
            orderItem.CalculateValue();
            _orderItems.Add(orderItem);
            CaculateValueOrder();            
        }

        public void RemoveItem(OrderItem orderItem)
        {
            if(!orderItem.IsValid()) return;

            var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId);

            if(existingItem == null) throw new DomainException("Item not found");

            _orderItems.Remove(existingItem);
            CaculateValueOrder();
        }   

        public void updateItem(OrderItem orderItem)
        {
            if(!orderItem.IsValid()) return;
            orderItem.AssociateOrder(Id);

            var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId);

            if(existingItem == null) throw new DomainException("Item not found");

            _orderItems.Remove(existingItem);
            _orderItems.Add(orderItem);

            CaculateValueOrder();
        }

        public void UpdateUnits(OrderItem orderItem, int units)
        {
            orderItem.UpdateUnit(units);
            updateItem(orderItem);
        }

        public void MakeDraft()
        {
            OrderStatus = OrderStatus.Draft;
        }   

        public void StartOrder()
        {
            OrderStatus = OrderStatus.Started;
        }

        public void FinishOrder()
        {
            OrderStatus = OrderStatus.Paid;
        }   

        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Canceled;
        }

        
        public static class OrderFactory
        {
            public static Order NewOrderDraft(Guid clientId)
            {
                var order = new Order
                {
                    ClientId = clientId
                };

                order.MakeDraft();
                return order;
            }
        }
    }
}
