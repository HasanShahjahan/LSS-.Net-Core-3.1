using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Interfaces;

namespace LSS.HCM.Core.Domain.Managers
{
    public class CompartmentManagement : ICompartmentManagement
    {
        private readonly ICommunicationPortControl _communicationPortControl;
        private readonly AppSettings _appSettings;
        public CompartmentManagement(ICommunicationPortControl communicationPortControl, AppSettings appSettings)
        {
            _communicationPortControl = communicationPortControl;
            _appSettings = appSettings;
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
