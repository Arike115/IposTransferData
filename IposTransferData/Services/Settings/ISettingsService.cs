﻿using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Settings
{
    public interface ISettingsService
    {
        Task<IEnumerable<Setting>> GetSettingsAsync();
        Task InsertSettingsAsync(Setting settings);
    }
}
