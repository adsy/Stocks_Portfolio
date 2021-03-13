using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Services.Data;
using Services.Models;

namespace Services.Configurations
{
    public class MapperInitialiser : Profile
    {

        // Country data class fields has a direct correlation to CountryDTO fields, which go in either direction (ReverseMap)
        public MapperInitialiser()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<ApiUser, UserDTO>().ReverseMap();

        }
    }
}
