using MediatR;
using Services.Data;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Stocks.Commands
{
    public class AddStockCommand : IRequest<StockDTO>
    {
        public StockDTO stock { get; set; }
    }
}