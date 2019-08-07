namespace ATypeScanner.Tests.Types
{
    public interface IAnInterface
    {
    }

    public interface IAnotherInterface : IAnInterface
    {
    }

    public class AnInterfaceImplementation : IAnInterface
    {

    }

    public class AnotherInterfaceImplementation : AnInterfaceImplementation
    {

    }
}
