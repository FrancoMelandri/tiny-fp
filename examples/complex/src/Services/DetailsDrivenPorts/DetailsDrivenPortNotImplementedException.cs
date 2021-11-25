using TinyFpTest.Configuration;

namespace TinyFpTest.Services.DetailsDrivenPorts
{
    public class DetailsDrivenPortNotImplementedException : Exception
    {
        public DetailsDrivenPortNotImplementedException(DetailsDrivenPortConfiguration detailsDrivenPortConfiguration)
            : base(detailsDrivenPortConfiguration.Adapter.ToString())
        {
        }
    }
}
