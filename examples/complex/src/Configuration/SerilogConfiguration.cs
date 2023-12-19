using Serilog.Events;
using System.Diagnostics.CodeAnalysis;

namespace TinyFpTest.Configuration;

[ExcludeFromCodeCoverage]
public class SerilogConfiguration
{
    public string Environment { get; set; }
    public string System { get; set; }
    public string Customer { get; set; }
    public string LogEventLevel { get; set; }
    public string SeqHost { get; set; }
    public string SeqApiKey { get; set; }
    public LogEventLevel MicrosoftLogEventLevel { get; set; } = Serilog.Events.LogEventLevel.Warning;
    public LogEventLevel SystemLogEventLevel { get; set; } = Serilog.Events.LogEventLevel.Warning;
}