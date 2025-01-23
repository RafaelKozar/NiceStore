using NiceStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Domain
{
    public class Dymensions
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public Dymensions(decimal height, decimal width, decimal depth)
        {
            AssertionConcern.ValidateIfLessThan(height, 1, "The height must be greater than 0");    
            AssertionConcern.ValidateIfLessThan(width, 1, "The width must be greater than 0");
            AssertionConcern.ValidateIfLessThan(depth, 1, "The depth must be greater than 0");

            Height = height;
            Width = width;
            Depth = depth;
        }

        public string FormatDymensions()
        {
            return $"H: {Height} x W: {Width} x D: {Depth}";    
        }

        public override string ToString()
        {
            return FormatDymensions();  
        }
    }
}
