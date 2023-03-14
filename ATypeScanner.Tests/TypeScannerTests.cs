using ATypeScanner.Tests.Types;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace ATypeScanner.Tests
{
    public class TypeScannerTests
    {
        [Fact]
        public void CanResolveOpenInterfaceTypes()
        {
            var scanner = new TypeScanner(typeof(AType).Assembly);
            var matches = scanner.FindClosingImplementationsOf(typeof(IOpenInterface<>)).ToList();
            matches.ShouldNotBeEmpty();
            matches.ShouldContain(p => p.ConcreteType == typeof(OpenInterfaceImplementation));
            matches.ShouldContain(p => p.ConcreteType == typeof(OpenClassImplementation));
            matches.ShouldAllBe(p => p.GenericType == typeof(IOpenInterface<AType>));
        }

        [Fact]
        public void CanResolveGenericOpenTypes()
        {
            var scanner = new TypeScanner(typeof(AType).Assembly);
            var matches = scanner.FindOpenImplementationsOf(typeof(IOpenInterface<>)).ToList();
            matches.ShouldNotBeEmpty();
            matches.ShouldContain(p => p.ConcreteType == typeof(GenericOpenInterfaceImplementation<>));
            matches.ShouldContain(p => p.ConcreteType == typeof(GenericOpenClassImplementation<>));
        }

        [Fact]
        public void CanResolveOpenClassTypes()
        {
            var scanner = new TypeScanner(typeof(AType).Assembly);
            var matches = scanner.FindClosingImplementationsOf(typeof(OpenClass<>));
            matches.ShouldNotBeEmpty();
            matches.ShouldContain(p => p.ConcreteType == typeof(OpenClassImplementation));
            matches.ShouldNotContain(p => p.ConcreteType == typeof(OpenInterfaceImplementation));
            matches.ShouldAllBe(p => p.GenericType == typeof(OpenClass<AType>));
        }

        [Fact]
        public void CanResolveInterfaceImplementations()
        {
            var scanner = new TypeScanner(typeof(AType).Assembly);
            var matches = scanner.FindImplementationsOf<IAnInterface>();
            matches.ShouldNotBeEmpty();
            matches.ShouldNotContain(typeof(IAnInterface));
            matches.ShouldNotContain(typeof(IAnotherInterface));
            matches.ShouldContain(typeof(AnInterfaceImplementation));
            matches.ShouldContain(typeof(AnotherInterfaceImplementation));
        }

        [Fact]
        public void CanResolveClassImplementations()
        {
            var scanner = new TypeScanner(typeof(AType).Assembly);
            var matches = scanner.FindImplementationsOf<AnInterfaceImplementation>();
            matches.ShouldNotBeEmpty();
            matches.ShouldNotContain(typeof(IAnInterface));
            matches.ShouldNotContain(typeof(IAnotherInterface));
            matches.ShouldNotContain(typeof(AnInterfaceImplementation));
            matches.ShouldContain(typeof(AnotherInterfaceImplementation));
        }
    }
}
