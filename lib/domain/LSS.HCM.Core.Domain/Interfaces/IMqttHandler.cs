using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Domain.Interfaces
{
    public interface IMqttHandler
    {
        void Scanner();
        void Ups();
        void Led();
    }
}
