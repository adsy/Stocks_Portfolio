using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class StockChartData
    {
        public StockChartData()
        {
            ChartDataList = new List<ChartData>();
        }

        public List<ChartData> ChartDataList { get; set; }
    }
}