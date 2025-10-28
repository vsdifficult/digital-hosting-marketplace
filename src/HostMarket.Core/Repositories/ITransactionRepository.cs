﻿using HostMarket.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Core.Repositories;
    public interface ITransactionRepository : IRepository<TransactionDto, Guid> { }
