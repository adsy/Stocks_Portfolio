using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioTrackerFunction.Infrastructure
{
    public class ServiceProcessResult
    {
        public int ServiceResultCode { get; set; }
        public string ServiceResultMessage { get; set; }
    }

    public class ServiceProcessResult<T> : ServiceProcessResult
    {
        public T Data { get; set; }
    }
}