using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Services.Configurations.Entities
{
    public class HotelConfiguration:IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel { Id = 1, Name = "Ibis Hotel", CountryId = 1, Address = "123 fake street", Rating = 4.4 },
                new Hotel { Id = 2, Name = "Mantra Hotel", CountryId = 3, Address = "123 fake street", Rating = 5.0 },
                new Hotel { Id = 3, Name = "Fake Hotel", CountryId = 2, Address = "123 fake street", Rating = 3.4 }
            );
        }
    }
}
