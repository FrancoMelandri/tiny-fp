using System.Diagnostics.CodeAnalysis;

namespace TinyFp;

[ExcludeFromCodeCoverage]
public struct Unit
{        
    public static readonly Unit Default = new();
}