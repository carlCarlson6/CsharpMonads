namespace Monads;

public class Operation { }

public class Successfull : Operation {}
public class Failure : Operation { }

public class Found : Operation {}
public class NotFound : Operation {}

public static class OperationExtensions
{
    public static bool IsSuccess(Operation operation) => operation switch
    {
        Successfull => true,
        Failure => false,
        _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
    };

    public static bool IsFound(Operation operation) => operation switch
    {
        Found found => throw new NotImplementedException(),
        NotFound notFound => throw new NotImplementedException(),
        _ => throw new ArgumentOutOfRangeException(nameof(operation))
    };
}