using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Services.Configurations.Entities
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {

            Stock[] stocksArray =
            {
                new Stock(1, "BB", DateTime.Parse("2021-02-01"), 14, 15, 210, "US"),
                new Stock(2, "SENS", DateTime.Parse("2021-02-04"), 2.65, 214.6241, 569.83, "US"),
                new Stock(3, "LOT", DateTime.Parse("2021-02-10"), .148, 3517, 499.97, "AU"),
            };

            builder.HasData(
                stocksArray
            );
        }
    }
}