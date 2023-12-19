using TinyFpTest.Configuration;

namespace TinyFpTest.Services.DetailsDrivenPorts;

public class DetailsDrivenPortNotImplementedException(DetailsDrivenPortConfiguration detailsDrivenPortConfiguration)
    : Exception(detailsDrivenPortConfiguration.Adapter.ToString());