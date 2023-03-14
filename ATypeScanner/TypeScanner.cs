using System;
using System.Collections.Generic;
using System.Reflection;

namespace ATypeScanner
{

    /// <summary>
    /// Resolve non generic (class) implementations of types or open types
    /// </summary>
    public class TypeScanner : ITypeScanner
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public TypeScanner(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        /// <summary>
        /// Returns all non generic classes that implement <paramref name="openType"/>
        /// </summary>
        public IEnumerable<ClosingTypeResult> FindClosingImplementationsOf(Type openType)
        {
            foreach (var assembly in _assemblies)
            {
                foreach (var type in assembly.DefinedTypes)
                {
                    if (type.IsGenericType || type.IsInterface || type.IsAbstract)
                    {
                        continue;
                    }
                    else if (openType.IsInterface)
                    {
                        foreach (var @interface in type.GetInterfaces())
                        {
                            if (@interface.IsGenericType && openType.IsAssignableFrom(@interface.GetGenericTypeDefinition()))
                            {
                                yield return new ClosingTypeResult(@interface, type.AsType());
                            }
                        }
                    }
                    else if (openType != type && MatchClassType(openType, type) is Type matchingType)
                    {
                        yield return new ClosingTypeResult(matchingType, type);
                    }
                }
            }
        }

        /// <summary>
        /// Returns all generic classes that implement <paramref name="openType"/>
        /// </summary>
        public IEnumerable<ClosingTypeResult> FindOpenImplementationsOf(Type openType)
        {
            foreach (var assembly in _assemblies)
            {
                foreach (var type in assembly.DefinedTypes)
                {
                    if (type.IsInterface || type.IsAbstract)
                    {
                        continue;
                    }
                    else if (type.IsGenericType) 
                    { 
                        if (openType.IsInterface)
                        {
                            foreach (var @interface in type.GetInterfaces())
                            {
                                if (@interface.IsGenericType && openType.IsAssignableFrom(@interface.GetGenericTypeDefinition()))
                                {
                                    yield return new ClosingTypeResult(@interface, type.AsType());
                                }
                            }
                        }
                        else if (openType != type && MatchClassType(openType, type) is Type matchingType)
                        {
                            yield return new ClosingTypeResult(matchingType, type);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns all classes that implement <typeparamref name="TType"/>
        /// </summary>
        public IEnumerable<Type> FindImplementationsOf<TType>()
        {
            return FindImplementationsOf(typeof(TType));
        }

        /// <summary>
        /// Returns all classes that implement <paramref name="typeToFind"/>
        /// </summary>
        public IEnumerable<Type> FindImplementationsOf(Type typeToFind)
        {
            foreach (var assembly in _assemblies)
            {
                foreach (var type in assembly.DefinedTypes)
                {
                    if (typeToFind != type && type.IsClass && typeToFind.IsAssignableFrom(type))
                    {
                        yield return type;
                    }
                }
            }
        }

        private Type MatchClassType(Type openType, Type test)
        {
            if (test == null)
            {
                return null;
            }

            if (test.IsGenericType && test.GetGenericTypeDefinition() == openType)
            {
                return test;
            }

            return MatchClassType(openType, test.BaseType);
        }
    }
}
