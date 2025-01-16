using System.Diagnostics.CodeAnalysis;

namespace TinyFpTest.Complex;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main()
        => Host.CreateDefaultBuilder([])
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build()
            .Run();
}