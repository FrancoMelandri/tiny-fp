﻿using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using TinyFp.Exceptions;

namespace TinyFp.Common;

public readonly partial struct Result<A>
{
    internal readonly ResultState State;
    internal readonly A Value;
    private readonly Exception exception;

    internal Exception Exception => exception ?? BottomException.Default;

    [Pure]
    public Result(A value)
    {
        State = ResultState.Success;
        Value = value;
        exception = default;
    }

    [Pure]
    public Result(Exception e)
    {
        State = ResultState.Faulted;
        exception = e;
        Value = default;
    }

    [Pure]
    public static implicit operator Result<A>(A value) 
        => new(value);

    [Pure]
    public bool IsFaulted 
        => State == ResultState.Faulted;

    [Pure]
    [ExcludeFromCodeCoverage]
    public bool IsBottom 
        => State == ResultState.Faulted && (exception == null || exception is BottomException);

    [Pure]
    public bool IsSuccess 
        => State == ResultState.Success;
}