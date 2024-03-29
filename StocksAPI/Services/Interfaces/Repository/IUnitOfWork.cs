﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Data;
using Services.IRepository;

namespace Services.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Stock> Stocks { get; }
        IGenericRepository<PortfolioTracker> PortfolioTrackers { get; }
        IGenericRepository<Cryptocurrency> Cryptocurrencies { get; }
        IGenericRepository<ApiUser> AspNetUsers { get; }
        IGenericRepository<SoldInstrument> SoldInstruments { get; }

        Task Save();
    }
}