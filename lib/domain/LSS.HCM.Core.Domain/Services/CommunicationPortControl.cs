using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Interfaces;
using System.Collections.Generic;
//using Compartment = LSS.HCM.Core.Entities.Locker.Compartment;

namespace LSS.HCM.Core.Domain.Services
{
    public class CommunicationPortControl : ICommunicationPortControl
    {
        private readonly ILockBoardInitializationCommand _lockBoardInitializationCommand;
        private readonly AppSettings _appSettings;
        private readonly SerialPortControl lockControlCommunicationPort;
        private readonly SerialPortControl ObjectDetectionCommunicationPort;
        private readonly SerialPortControl ScannerCommunicationPort;
        public CommunicationPortControl(ILockBoardInitializationCommand lockBoardInitializationCommand, AppSettings appSettings)
        {
            _lockBoardInitializationCommand = lockBoardInitializationCommand;
            _appSettings = appSettings;
            lockControlCommunicationPort = new SerialPortControl(_appSettings);
            ObjectDetectionCommunicationPort = new SerialPortControl(_appSettings);
            ScannerCommunicationPort = new SerialPortControl(_appSettings);
        }
        public Dictionary<string, string> SendCommand(string commandName, List<byte> commandData)
        {
            List<byte> commandString = _lockBoardInitializationCommand.GenerateCommandBuffer(commandName, commandData);
            SerialPortControl controlModule;
            List<byte> responseData;
            Dictionary<string, string> result = null;
            if (commandName == CommandType.DoorOpen)
            {
                controlModule = lockControlCommunicationPort;
                responseData = controlModule.WriteAndWait(commandString, _appSettings.Microcontroller.Commands.OpenDoor.ResLen);
                result = _lockBoardInitializationCommand.ExecuteCommandResponse(CommandType.DoorOpen, responseData);
            }
            else if (commandName == CommandType.DoorStatus)
            {
                //controlModule = lockControlCommunicationPort;
                //responseData = controlModule.WriteAndWait(commandString, _appSettings.Microcontroller.Commands.DoorStatus.ResLen);
                //result = _lockBoardInitializationCommand.ExecuteCommandResponse(CommandType.DoorStatus, responseData);
            }
            else if (commandName == CommandType.ItemDetection)
            {
                //controlModule = ObjectDetectionCommunicationPort;
                //responseData = controlModule.WriteAndWait(commandString, _appSettings.Microcontroller.Commands.DetectItem.ResLen);
                //result = _lockBoardInitializationCommand.ExecuteCommandResponse(CommandType.ItemDetection, responseData);
            }

            Dictionary<string, string> commandResult = new Dictionary<string, string>();
            commandResult.Add("Command", commandName);
            commandResult.Add("DoorOpen", result.ToString());
            commandResult.Add("DoorAvailable", result.ToString());

            return commandResult;
        }
    }
}
