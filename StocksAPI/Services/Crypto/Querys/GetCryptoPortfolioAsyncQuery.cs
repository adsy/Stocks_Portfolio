﻿using MediatR;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Crypto.Querys
{
    public class GetCryptoPortfolioAsyncQuery : IRequest<Response<CryptoPortfolio>>
    {
    }
}