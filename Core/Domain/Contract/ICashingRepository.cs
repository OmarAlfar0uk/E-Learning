﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    public interface ICashingRepository
    {
        // get
        Task<string?> GetAsync(string CasheKey);

        // set  
        Task SetAsync(string CasheKey, string CacheValue, TimeSpan TimeToLive);
    }
}
