using NiceStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalogo.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }    
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Price { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string Image { get; private set; }
        public int StockQuantity { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        public Product(string name, string description, bool active, decimal price, Guid categoryId, DateTime registerDate, string image)
        {
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            RegisterDate = registerDate;
            Image = image;            
            CategoryId = categoryId;
        }

        public void Activate() => Active = true;

        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

    }
}
