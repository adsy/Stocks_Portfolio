using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Services.Data;
using Services.Models;
using Services.Models.Crypto;
using Services.Models.SoldInstruments;
using Services.Models.Stocks;

namespace Services.Configurations
{
    public class MapperInitialiser : Profile
    {
        // Country data class fields has a direct correlation to CountryDTO fields, which go in either direction (ReverseMap)
        public MapperInitialiser()
        {
            CreateMap<ApiUser, UserDTO>().ReverseMap();
            CreateMap<Stock, StockDTO>().ReverseMap();
            CreateMap<Stock, StockInfo>().ReverseMap();
            CreateMap<PortfolioTracker, PortfolioTrackerDTO>().ReverseMap();
            CreateMap<Cryptocurrency, CryptocurrencyDTO>().ReverseMap();
            CreateMap<Cryptocurrency, CoinInfo>().ReverseMap();
            CreateMap<SoldInstrument, SoldInstrumentDTO>().ReverseMap();
        }
    }
}