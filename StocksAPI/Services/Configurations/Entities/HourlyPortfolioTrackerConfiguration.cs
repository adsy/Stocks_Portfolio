using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Configurations.Entities
{
    public class HourlyPortfolioTrackerConfiguration : IEntityTypeConfiguration<PortfolioTracker>
    {
        public void Configure(EntityTypeBuilder<PortfolioTracker> builder)
        {
            PortfolioTracker[] portfolioTotalArray =
            {
                new PortfolioTracker{Id=1, PortfolioTotal = 2000.00,TimeStamp = DateTime.Now.AddHours(-1) },
                new PortfolioTracker{Id=2, PortfolioTotal = 2000.00,TimeStamp = DateTime.Now }
            };

            builder.HasData(portfolioTotalArray);
        }
    }
}