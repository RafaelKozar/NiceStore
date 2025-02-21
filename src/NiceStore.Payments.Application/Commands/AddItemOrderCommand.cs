﻿using FluentValidation;
using NiceStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NiceStore.Payments.Application.Commands
{
    public class AddItemOrderCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }     
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal ValueUnity { get; private set; }

        public AddItemOrderCommand(Guid customerId, Guid orderId,  string productName, int quantity, decimal value)
        {
            CustomerId = customerId;
            OrderId = orderId;         
            ProductName = productName;
            Quantity = quantity;
            ValueUnity = value;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddItemOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddItemOrderValidation : AbstractValidator<AddItemOrderCommand>
    {
        public AddItemOrderValidation()
        {            
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid client ID");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid order ID");

            RuleFor(c => c.ProductName)
                .NotEmpty()
                .WithMessage("Product name is required");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Minimum quantity of an item is 1");

            RuleFor(c => c.ValueUnity)
                .GreaterThan(0)
                .WithMessage("Item value must be greater than 0");
               
        }
    }   
}
