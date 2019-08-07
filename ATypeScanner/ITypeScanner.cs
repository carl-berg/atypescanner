using System;
using System.Collections.Generic;

namespace ATypeScanner
{
    public interface ITypeScanner
    {
        IEnumerable<ClosingTypeResult> FindClosingImplementationsOf(Type openType);
        IEnumerable<Type> FindImplementationsOf<TType>();
        IEnumerable<Type> FindImplementationsOf(Type typeToFind);
    }
}
