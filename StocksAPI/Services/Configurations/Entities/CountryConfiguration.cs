using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Services.Configurations.Entities
{
    public class CountryConfiguration: IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country {Id = 1, Name = "Australia", ShortName = "AUS"},
                new Country {Id = 2, Name = "New Zealand", ShortName = "NZ"},
                new Country {Id = 3, Name = "Singapore", ShortName = "SG"}
            );
        }
    }
}
