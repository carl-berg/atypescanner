namespace ATypeScanner.Tests.Types
{
    public interface IOpenInterface<T>
    {
    }

    public class OpenInterfaceImplementation : IOpenInterface<AType>
    {

    }

    public abstract class OpenClass<T> : IOpenInterface<T>
    {

    }

    public class OpenClassImplementation : OpenClass<AType>
    {

    }

    public class AType
    {

    }
}
