namespace Monads;

public class Result<T>
{
    internal Result() { }

    public static Result<T> Ok(T value) => new OkResult<T>(value);
    public static Result<T> Ko(Error error) => new KoResult<T>(error);
}

internal sealed class OkResult<T> : Result<T>
{
    internal readonly T Value;

    internal OkResult(T value) => Value = value;
}

internal sealed class KoResult<T> : Result<T>
{
    internal readonly Error Error;

    internal KoResult(Error error) => Error = error;
}

public class Error : Exception
{
    public Error(string message) : base(message) { }
}

public static class ResultExtensions
{
    public static Result<TOut> Map<T, TOut>(this Result<T> result, Func<T, TOut> mapper) => result switch
    {
        KoResult<T> koResult => new KoResult<TOut>(koResult.Error),
        OkResult<T> okResult => Result<TOut>.Ok(mapper(okResult.Value)),
        _ => throw new ArgumentOutOfRangeException(nameof(result))
    };
    
    public static TOut Match<T, TOut>(this Result<T> result, Func<T, TOut> onOkResult, Func<Error, TOut> onKoResult) => result switch
    {
        KoResult<T> koResult => onKoResult(koResult.Error),
        OkResult<T> okResult => onOkResult(okResult.Value),
        _ => throw new ArgumentOutOfRangeException(nameof(result))
    };

    public static Result<T> UnWrap<T>(this Result<Result<T>> resultResult) => resultResult switch
    {
        KoResult<Result<T>> koResult => new KoResult<T>(koResult.Error),
        OkResult<Result<T>> okResult => okResult.Value,
        _ => throw new ArgumentOutOfRangeException(nameof(resultResult))
    };
}