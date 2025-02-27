using MediatR;
using NiceStore.Payments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Payments.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddItemOrderCommand, bool>
    {
        private readonly IOrderRepository _ordererRepository;

        public OrderCommandHandler(IOrderRepository ordererRepository)
        {
            _ordererRepository = ordererRepository;
        }

        public async Task<bool> Handle(AddItemOrderCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;
            var order = await _ordererRepository.GetDraftOrderByClientId(request.ClientId);
            var orderItem = new OrderItem(request.ProductId, request.ProductName, request.Quantity, request.ValueUnity);

            if(order == null)
            {
                order =  Order.OrderFactory.NewOrderDraft(request.ClientId);
                order.AddItem(orderItem);

                _ordererRepository.Add(order);
            }
            else
            {
                var orderItemExist = order.OrderItemExists(orderItem);
                order.AddItem(orderItem);

                if(orderItemExist)
                {
                    _ordererRepository.UpdateItem(order.OrderItems.FirstOrDefault(x => x.ProductId == orderItem.ProductId));  
                }
                else
                {
                    _ordererRepository.AddItem(orderItem);
                }
            }

            return await _ordererRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(AddItemOrderCommand command)
        {
            if (command.IsValid()) return true;

            foreach (var error in command.ValidationResult.Errors)
            {
                // Publish event
            }

            return false;
        }
    }
}
