using LSS.HCM.Core.Entities.Locker;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Domain.Interfaces
{
    public interface ICompartmentManagement
    {
        Compartment DoorOpen(string compartmentId);
    }
}
