namespace Monads;

public class Maybe<T>
{
    internal Maybe() { }

    private static Maybe<T> Create(T? value) => value is null ? new None<T>() : new Some<T>(value);

    public static Maybe<T> Some(T value) => Create(value);
    public static Maybe<T> None() => new None<T>();
}

internal sealed class Some<T> : Maybe<T>
{
    internal readonly T Value;

    internal Some(T value) => Value = value;
}

internal sealed class None<T> : Maybe<T> { }

public static class MaybeExtensions
{
    public static Maybe<TOut> Map<T, TOut>(this Maybe<T> maybe, Func<T, TOut> mapper) => maybe switch
    {
        None<T> => new None<TOut>(),
        Some<T> some => Maybe<TOut>.Some(mapper(some.Value)),
        _ => throw new ArgumentOutOfRangeException(nameof(maybe))
    };
    
    public static TOut Match<T, TOut>(this Maybe<T> maybe, Func<T, TOut> onSome, Func<TOut> onNone) => maybe switch
    {
        None<T> => onNone(),
        Some<T> some => onSome(some.Value),
        _ => throw new ArgumentOutOfRangeException(nameof(maybe))
    };

    public static Maybe<T> UnWrap<T>(this Maybe<Maybe<T>> maybeMaybe) => maybeMaybe switch
    {
        None<Maybe<T>> => new Maybe<T>(),
        Some<Maybe<T>> some => some.Value,
        _ => throw new ArgumentOutOfRangeException(nameof(maybeMaybe))
    };
}

