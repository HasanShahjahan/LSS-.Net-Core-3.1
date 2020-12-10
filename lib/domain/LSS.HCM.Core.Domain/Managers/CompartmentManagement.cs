using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Domain.Interfaces;

namespace LSS.HCM.Core.Domain.Managers
{
    public class CompartmentManagement : ICompartmentManagement
    {
        private readonly ICommunicationPortControl _communicationPortControl;
        public CompartmentManagement(ICommunicationPortControl communicationPortControl)
        {
            _communicationPortControl = communicationPortControl;
        }
        public void DoorOpen(string compartmentId)
        {
            _communicationPortControl.SendCommand(CommandType.DoorOpen, GetCompartmentPinCode());
        }

        private string GetCompartmentPinCode()
        {
            return string.Empty;
        }
    }
}
