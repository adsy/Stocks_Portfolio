using AutoMapper;
using Services.Interfaces.Repository;
using Services.IRepository;
using Services.Models.Portfolio;
using Services.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.PortfolioRepository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStocksRepository _stocksRepository;
        private readonly ICryptoRepository _cryptoRepository;

        public PortfolioRepository(IUnitOfWork unitOfWork, IMapper mapper, IStocksRepository stocksRepository, ICryptoRepository cryptoRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _stocksRepository = stocksRepository ?? throw new ArgumentNullException(nameof(stocksRepository));
            _cryptoRepository = cryptoRepository ?? throw new ArgumentNullException(nameof(cryptoRepository));
        }

        public async Task<CompletePortfolio> GetPortfolio()
        {
            var fnResult = new CompletePortfolio();

            fnResult.CurrentStockPortfolio = await _stocksRepository.GetStockPortfolioAsync();
            fnResult.CurrentCryptoPortfolio = await _cryptoRepository.GetCryptoPortfolioAsync();

            // loop through and pull out current values and profit amounts for fnResult.PortfolioProfit property

            return fnResult;
        }
    }
}