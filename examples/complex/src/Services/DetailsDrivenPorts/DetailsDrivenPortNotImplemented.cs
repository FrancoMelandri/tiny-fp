using System;
using TinyFpTest.Configuration;

namespace TinyFpTest.Services.DetailsDrivenPorts
{
    public class DetailsDrivenPortNotImplemented : Exception
    {
        public DetailsDrivenPortNotImplemented(DetailsDrivenPortConfiguration detailsDrivenPortConfiguration)
            : base(detailsDrivenPortConfiguration.Adapter.ToString())
        {
        }
    }
}
