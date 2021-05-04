using System.Diagnostics.CodeAnalysis;

namespace TinyFpTest.Configuration
{
    [ExcludeFromCodeCoverage]
    public class FileLogConfiguration
    {
        public string LogsFileName { get; set; }
        public int RetainedFileCountLimit { get; set; }
        public long FileSizeLimitBytes { get; set; }
        public bool Enabled { get; set; }
    }

}
