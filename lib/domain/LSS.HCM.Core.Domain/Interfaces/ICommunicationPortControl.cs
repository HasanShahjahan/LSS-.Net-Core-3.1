using LSS.HCM.Core.Entities.Locker;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Domain.Interfaces
{
    public interface ICommunicationPortControl
    {
        Dictionary<string, string> SendCommand(string commandName, List<byte> commandData);
    }
}
