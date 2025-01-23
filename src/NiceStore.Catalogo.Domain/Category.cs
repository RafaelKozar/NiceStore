using NiceStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Domain
{
    public class Category : Entity
    {        
        public string Name { get; private set; }    
        public int Code { get; private set; }

        //EF Relation   
        public ICollection<Product> Products { get; set; }  

        protected Category() { }
        public Category(string name, int code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }   

    }
}
