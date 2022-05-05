using Monads;

namespace Sample.TodoApp.Abstractions;

public interface IDomainService<in T, TR>
{
    public Task<Result<TR>> Execute(T input);
}