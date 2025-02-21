using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Payments.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddItemOrderCommand, bool>
    {
        public async Task<bool> Handle(AddItemOrderCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;
            return true;
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
