using System;

namespace ATypeScanner
{
    public class ClosingTypeResult
    {
        public ClosingTypeResult(Type genericType, Type concreteType)
        {
            GenericType = genericType;
            ConcreteType = concreteType;
        }

        public Type GenericType { get; }
        public Type ConcreteType { get; }
    }
}
