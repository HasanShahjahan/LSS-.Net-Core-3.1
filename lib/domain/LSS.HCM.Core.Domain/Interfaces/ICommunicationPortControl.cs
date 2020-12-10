using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Domain.Interfaces
{
    public interface ICommunicationPortControl
    {
        string SendCommand(string commandName, string commandData);
    }
}
