using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servcom.SGA.Service.Api.Configurations
{
    public class SignalRConfiguration : Hub
    {
        public Task SendToAll(string name, string message)
        {
            return Clients.All.SendAsync("sendToAll", name, message);
        }
    }
}
