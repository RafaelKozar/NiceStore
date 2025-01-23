using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Core.DomainObjects
{
    public class AssertionConcern
    {
        public static void ValidateIfEqual(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
                throw new DomainException(message);
        }   

        public static void ValidateIfDifferent(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
                throw new DomainException(message);
        }   

        public static void ValidateIfDifferent(string value1, string value2, string message)
        {
            if (value1 == value2)
                throw new DomainException(message);
        }

        public static void ValidateIfNull(object object1, string message)
        {
            if (object1 == null)
                throw new DomainException(message);
        }   
        
        public static void ValidateIfLessThan(int value1, int value2, string message)
        {
            if (value1 < value2)
                throw new DomainException(message);
        }   

        public static void ValidateIfLessThan(decimal value1, decimal value2, string message)
        {
            if (value1 < value2)
                throw new DomainException(message);
        }
        

        public static void ValidateIfFalse(bool boolValue, string message)
        {
            if (!boolValue)
                throw new DomainException(message);
        }   

        public static void ValidateIfTrue(bool boolValue, string message)
        {
            if (boolValue)
                throw new DomainException(message);
        }
    }
}
