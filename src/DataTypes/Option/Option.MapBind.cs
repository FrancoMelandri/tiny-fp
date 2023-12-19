using System.Diagnostics.Contracts;

namespace TinyFp;

public readonly partial struct Option<A>
{
    [Pure]
    public Option<B> Map<B>(Func<A, B> map)
        => _isSome ? Option<B>.Some(map(_value)) :
            Option<B>.None();

    [Pure]
    public Option<A> MapNone(Func<A> map)
        => _isSome ? Some(_value) :
            Some(map());

    [Pure]
    public async Task<Option<B>> MapAsync<B>(Func<A, Task<B>> mapAsync)
        => _isSome ? Option<B>.Some(await mapAsync(_value)) :
            Option<B>.None();

    [Pure]
    public async Task<Option<A>> MapNoneAsync(Func<Task<A>> mapAsync)
        => _isSome ? Some(_value) :
            Some(await mapAsync());

    [Pure]
    public Option<B> Bind<B>(Func<A, Option<B>> bind)
        => _isSome ? bind(_value) : Option<B>.None();

    [Pure]
    public Option<A> BindNone(Func<Option<A>> bind)
        => _isSome ? Some(_value) :
            bind();

    [Pure]
    public async Task<Option<B>> BindAsync<B>(Func<A, Task<Option<B>>> bindAsync)
        => _isSome ? await bindAsync(_value) : Option<B>.None();

    [Pure]
    public async Task<Option<A>> BindNoneAsync(Func<Task<Option<A>>> bind)
        => _isSome ? Some(_value) :
            await bind();
}