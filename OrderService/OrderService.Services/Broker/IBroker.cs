using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Services.Broker
{
    public interface IBroker
    {
        void SendMessage(string message);
    }
}
