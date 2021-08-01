using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioTrackerFunction.Infrastructure.Models
{
    public class ExchangeRate : TableEntity
    {
        public double rate { get; set; }
    }
}