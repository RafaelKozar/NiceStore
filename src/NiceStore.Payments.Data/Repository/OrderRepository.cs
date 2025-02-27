using Microsoft.EntityFrameworkCore;
using NiceStore.Core.Data;
using NiceStore.Payments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Payments.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {   
        public readonly PaymentsContext _context;

        public OrderRepository(PaymentsContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void AddItem(OrderItem orderItem)
        {
            _context.Add(orderItem);
        }

        public async Task<Order> GetDraftOrderByClientId(Guid clientId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.ClientId == clientId && o.OrderStatus == OrderStatus.Draft);
            if (order == null) return null;

            await _context.Entry(order)
                .Collection(i => i.OrderItems).LoadAsync();

            if(order.VoucherId != null)
            {
                await _context.Entry(order)
                    .Reference(i => i.Voucher).LoadAsync();
            }

            return order;
        }

        public async Task<OrderItem> GetItemById(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);  
        }

        public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(i => i.OrderId == orderId && i.ProductId == productId);
        }

        public async Task<IEnumerable<Order>> GetListByClientId(Guid clientId)
        {
            return await _context.OrderItems.AsNoTracking()
                .Include(i => i.Order)
                .Where(i => i.Order.ClientId == clientId)
                .Select(i => i.Order)
                .ToListAsync(); 
        }

        public Task<Voucher> GetVoucherByCode(string code)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
