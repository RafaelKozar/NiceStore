﻿using NiceStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }    
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Image { get; private set; }
        public int StockQuantity { get; private set; }
        public Guid CategoryId { get; private set; }
        public Dimensions Dimensions { get; private set; }
        
        public Category Category { get; private set; }

        public Product(string name, string description, bool active, decimal price, Guid categoryId, DateTime registerDate, string image, Dimensions dymensions)
        {
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            CreatedAt = registerDate;
            Image = image;
            CategoryId = categoryId;

            Validate();
            Dimensions = dymensions;
        }

        protected Product() { }

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }   

        public void SubtractStock(int quantity)
        {
            if (quantity < 0)
                quantity *= -1;

            if (!HasStock(quantity))
                throw new DomainException("Insufficient stock");

            StockQuantity -= quantity;
        }   

        public void AddStock(int quantity)
        {
            StockQuantity += quantity;
        }

        public bool HasStock(int quantity)
        {
            return StockQuantity >= quantity;
        }   

        public void Validate()
        {
            AssertionConcern.ValidateIfNull(Name, "The product name cannot be null");            
        }

    }
}
