﻿using IposTransferData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Services.Clients
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetClientsAsync();
        Task InsertClientAsync(Client client);
    }
}
