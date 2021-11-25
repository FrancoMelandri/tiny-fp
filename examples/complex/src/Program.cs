using System.Diagnostics.CodeAnalysis;

namespace TinyFpTest.Complex
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main()
            => Host.CreateDefaultBuilder(Array.Empty<string>())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .Build()
                .Run();
    }
}
