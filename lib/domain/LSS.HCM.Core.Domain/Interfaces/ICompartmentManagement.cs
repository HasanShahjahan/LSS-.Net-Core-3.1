using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Domain.Interfaces
{
    public interface ICompartmentManagement
    {
        void DoorOpen(string compartmentId);        
    }
}
